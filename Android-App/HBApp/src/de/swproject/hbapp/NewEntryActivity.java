/*
    This file is part of HaushaltsRechner.

    HaushaltsRechner is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    HaushaltsRechner is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with HaushaltsRechner.  If not, see <http://www.gnu.org/licenses/>.

    Diese Datei ist Teil von HaushaltsRechner.

    HaushaltsRechner ist Freie Software: Sie können es unter den Bedingungen
    der GNU General Public License, wie von der Free Software Foundation,
    Version 3 der Lizenz oder (nach Ihrer Option) jeder späteren
    veröffentlichten Version, weiterverbreiten und/oder modifizieren.

    HaushaltsRechner wird in der Hoffnung, dass es nützlich sein wird, aber
    OHNE JEDE GEWÄHELEISTUNG, bereitgestellt; sogar ohne die implizite
    Gewährleistung der MARKTFÄHIGKEIT oder EIGNUNG FÜR EINEN BESTIMMTEN ZWECK.
    Siehe die GNU General Public License für weitere Details.

    Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
    Programm erhalten haben. Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/

package de.swproject.hbapp;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.*;

import de.swproject.hbapp.business.*;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.os.Bundle;
import java.text.ParseException;
import java.text.SimpleDateFormat;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.Spinner;
import android.widget.TextView;

/**
 * Klasse zum Erstellen eines neuen Buchungseintrages
 * Activity, welche beim Starten der App angezeigt wird
 * @author Franziska Staake
 *
 */
@SuppressLint("SimpleDateFormat") 
public class NewEntryActivity extends Activity{
	/**
	 * globale Variablen
	 */
	//benötigt für Datepicker-Dialoge
	private static int year;
	private static int month;
	private static int day;
 
	private String fileName;	
	
	//View-Elemente
	private TextView tvAmount;
	private TextView tvReason;
	private Spinner spinnerAccount;
	private TextView tvDate; 
	private Spinner spinnerCategory;
	private DataHelper helper;	
	
	/**
	 * Konstanten
	 */
	private static final int DATE_DIALOG_ID = 999;	
	private static final String TAG = "MyActivity";
	

	/**
	 * Methode welche beim Werfen des onCreate-Events ausgeführt wird
	 * @param	savedInstanceState	Bundle, welches den Status der Activity enthält
	 */
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Bestimmen des Layouts
        setContentView(R.layout.new_entry_activity);
        helper = new DataHelper();
        
        //Methodenaufrufe
        
        //Spinner erstellen
        setAccountSpinner("accounts.json");        
        setCategorySpinner("categories.json");   
        
        //Listener
        addListenerOnButtonSave(); 
    	addListenerOnTextView();
    	addListenerOnButtonClear();
    	
    	//Datumsfeld mit aktuellem Datum füllen
    	setCurrentDateOnView();    	
    }
	
	//-----------------------------------SPEICHERN LÖSCHEN-------------------------------------------
	//-----------------------------------------------------------------------------------------------
	
	/**
	 * Dient dem Speichern einer Buchung
	 * @throws IOException
	 * @throws ParseException
	 */
	private void saveItems(){
		//Views initialisieren/deklarieren
		declareTextViews();
		Booking booking = new Booking();
		//neuen Dateinamen erstellen
		fileName = UUID.randomUUID().toString() + ".txt";
		
		//Booking initialisieren
		booking.account = (Account) spinnerAccount.getSelectedItem();
		booking.category = (Category) spinnerCategory.getSelectedItem();
		booking.amount = Double.parseDouble(tvAmount.getText().toString());
		booking.reason = tvReason.getText().toString();
		booking.user = getUser();
		try {
			booking.date = new SimpleDateFormat("dd-MM-yyyy").parse(tvDate.getText().toString());
		} catch (ParseException e) {
			Log.e(TAG, "parse()", e);
		}
		booking.fileName = fileName;
		
		//Datei erstellen
		FileOutputStream fos = null;
		try {
			fos = openFileOutput(booking.fileName, MODE_PRIVATE);
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		//Buchung in Datei speichern
		booking.saveBookingIntoFile(booking, fos);
		
		//Textfelder leeren, Spinner/Datum zurücksetzen
		clearFields();				
	}
		

	/**
	 * Dient dem Leeren der Textfelder und Zurücksetzen der Spinner und des Datums
	 */
	private void clearFields() {
		//Views deklarieren/initalisieren, da evtl. noch nicht erfolgt
		declareTextViews();
		spinnerAccount.setSelection(0);
		spinnerCategory.setSelection(0);
		tvAmount.setText("");
		tvReason.setText("");
		setCurrentDateOnView();		
	}

	/**
	 * alle Dateien in data/data/files löschen
	 */
	private void deleteFiles(){
		//Stamverzeichnis
		File directory = getFilesDir();
		//Liste der Dateien
		String[] list = directory.list();
		
		for(String s:list){
			deleteFile(s);
		}
	}
	
	
	
	//-----------------------------------------------------------------------------------------------
	//----------------------------------CREATION-------------------------------------------------------
	
	/**
	 * Benutzer des Gerätes bestimmen
	 * @return User
	 */
	private User getUser() {
		User user = new User();
		FileInputStream fis = null;
		try {
			fis = openFileInput("user.json");
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		user = user.getCurrentUser(fis);
		
		return user;
	}
	
	/**
	 * Dient der Deklaration und Initialisierung der Views
	 */
	private void declareTextViews() {
		spinnerAccount = (Spinner)findViewById(R.id.spinnerAccount);
		spinnerCategory = (Spinner)findViewById(R.id.spinnerCategory);
		tvAmount = (TextView)findViewById(R.id.tbAmount);
		tvReason = (TextView)findViewById(R.id.tbReason);
		tvDate = (TextView)findViewById(R.id.tbDate);
	}
	
	//---------------------------------SPINNER----------------------------------------------
		//--------------------------------------------------------------------------------------
		/**
	 * Erstellen des Spinners für die Konten
	 * @param fileName Dateiname
	 */
	private void setAccountSpinner(String fileName) {
		//Datei holen
		FileInputStream fis = null;        
        try {
			fis = openFileInput(fileName);
		} catch (FileNotFoundException e) {
			Log.e(TAG, "FileNotFound", e);
		}
        //Objekt, an dem die Klasse in "readFileContent" gelesen wird
        Account[] aClass = new Account[100];
        //Array, welcher die Daten für den Spinner enthält
        Account[] accounts = (Account[]) helper.readFileContent(fis, aClass);
        //Spinner erstellen
        Spinner aSpinner = (Spinner)findViewById(R.id.spinnerAccount);
        //Spinner mit Daten füllen
        helper.fillSpinnerWithData(aSpinner, this, accounts);		
	}
	
	/**
	 * Erstellen des Spinners für die Kategorien
	 * @param fileName Dateiname
	 */
	private void setCategorySpinner(String fileName) {
		//Datei holen
		FileInputStream fis = null;
		try {
			fis = openFileInput(fileName);
		} catch (FileNotFoundException e) {
			Log.e(TAG, "FileNotFound", e);
		}
		//Objekt, an dem die Klasse in "readFileContent" gelesen wird
        Category[] cClass = new Category[100];
        //Array, welcher die Daten für den Spinner enthält
        Category[] categories = (Category[]) helper.readFileContent(fis, cClass);        
        Spinner cSpinner = (Spinner)findViewById(R.id.spinnerCategory);
        //Spinner mit Daten füllen
        helper.fillSpinnerWithData(cSpinner, this, categories);  	
	}
	
	//----------------------------------------------------------------------------------------------
	//------------------------------------LISTENER---------------------------------------------------

	/**
	 * Listener für den Button "Textfelder leeren"
	 */
	private void addListenerOnButtonClear() {
		//Button initialisieren
		Button btDelete = (Button)findViewById(R.id.btClear);
		//Listener setzen
		btDelete.setOnClickListener(new View.OnClickListener() {			 
			public void onClick(View v) { 
					//Methode ausführen, wenn auf Button geklickt wurde
					clearFields();
			}
		});			
	}

	/**
	 * Listener für das Datumsfeld
	 */
	private void addListenerOnTextView() {
		//Textfeld initialisieren
		tvDate = (TextView)findViewById(R.id.tbDate);
		//Listener setzen
        tvDate.setOnClickListener(new View.OnClickListener() {			 
			public void onClick(View v) { 
				//Methode ausführen, wenn auf Textfeld geklickt wurde
				showDialog(DATE_DIALOG_ID); 
			}
		});		
		
	}

	/**
	 * Listener für den Button "Speichern"
	 */
	private void addListenerOnButtonSave() {
		//Button initialisieren
		Button btSave = (Button)findViewById(R.id.btSave);
		//Listener setzen
		btSave.setOnClickListener(new View.OnClickListener() {			 
			public void onClick(View v) { 				
				//Methode ausführen, wenn auf Button geklickt wurde
				saveItems();
			}
		});	
	}
	
	
	//-----------------------------------------------------------------------------------------------
	//-------------------------------------DATEPICKER-------------------------------------------------

	/**
	 * Dient dem Eintragen des aktuellen Datums in das Datumsfeld
	 */
	public void setCurrentDateOnView() { 
		//aktuelles Datum holen
		String dateString = setCurrentDate(); 
		tvDate.setText(dateString);		
	}
	
	/**
	 * Dient dem erstellen des Datums-Strings 
	 * mit dem aktuellen Datum für das Datumsfeld
	 * @return Datums-String
	 */
	public static String setCurrentDate(){
		//aktuelles Datum lesen
		final Calendar c = Calendar.getInstance();
		year = c.get(Calendar.YEAR);
		month = c.get(Calendar.MONTH);
		day = c.get(Calendar.DAY_OF_MONTH);	
				
		//String erstellen
		StringBuilder dateString = new StringBuilder()
			// Monat fängt mit 0 an -> 1 addieren
			.append(day)
			.append("-")
			.append(month + 1)
			.append("-")
			.append(year);
		
		return dateString.toString();
	}
	
	/**
	 * Dient dem Erstellen des DatePickerDialoge
	 * @param id ID des DatePickers
	 * @return Dialog
	 */
	@Override
	protected Dialog onCreateDialog(int id) {
		switch (id) {
		   case DATE_DIALOG_ID:
		   //aktuelles Datum setzen
		   return new DatePickerDialog(this, datePickerListener, year, month, day);
		}
		return null;
	}
 
	/**
	 * Dient dem Setzen des Listeners für den DatePickerDialoge
	 * Listener wird ausgelöst, wenn das Datum geändert wird
	 */
	private DatePickerDialog.OnDateSetListener datePickerListener 
                = new DatePickerDialog.OnDateSetListener() {
 
		//ausgelöst, wenn DialogBox geschlossen ist
		public void onDateSet(DatePicker view, int selectedYear,
				int selectedMonth, int selectedDay) {
			year = selectedYear;
			month = selectedMonth;
			day = selectedDay;
 
			//Datum wird ins Datumsfeld geschrieben
			tvDate.setText(new StringBuilder()
			.append(day)
			.append("-")
			.append(month + 1)
			.append("-").append(year)
			.append(" "));
		}
	};
	
//--------------------------------------------------------------------------------------------------	
//---------------------MENUE-------------------------------------------------------------------------
	
	
	/**
	 * Menu-erstellung
	 * @param menu 
	 * @return true
	 */
	@Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_start_activity, menu);
        return true;
    }
	
	
    /**
     * Reaktion auf MenüAuswahl
     * @param item Menü-Auswahl
     * @return true
     */
    public boolean onOptionsItemSelected(MenuItem item) {
    	switch (item.getItemId()) {
    		//App verlassen
    		case R.id.app_exit: this.finish();
    		break;
    		//Dateien löschen
    		case R.id.deleteFiles: deleteFiles();
    	}
		return true;
    }
}
