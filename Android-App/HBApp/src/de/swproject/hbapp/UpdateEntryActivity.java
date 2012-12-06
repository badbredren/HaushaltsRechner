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
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.UUID;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.Spinner;
import android.widget.TextView;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

import de.swproject.hbapp.business.Account;
import de.swproject.hbapp.business.Booking;
import de.swproject.hbapp.business.Category;
import de.swproject.hbapp.business.DataHelper;

/**
 * Klasse zum Aktualisieren eines neuen Buchungseintrages
 * @author Franziska Staake
 *
 */
public class UpdateEntryActivity extends Activity{

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
	private Spinner tvAccount;
	private TextView tvDate; 
	private Spinner tvCategory;
	private DataHelper helper;
	
	/**
	 * Konstanten
	 */
	private static final String TAG = "MyActivity";
	static final int DATE_DIALOG_ID = 999;
	
	/**
	 * Methode welche beim Werfen des onCreate-Events ausgeführt wird
	 * @param	savedInstanceState	Bundle, welches den Status der Activity enthält
	 */
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        //Bestimmen des Layouts
        setContentView(R.layout.update_entry_activity);
        helper = new DataHelper();
        
        //Spinner erstellen
        setAccountSpinner("accounts.json");        
        setCategorySpinner("categories.json");        
        
        //aktuelles Datum für DatePickerDialoge
        setCurrentDate();
        //Listener
        addListenerOnButtonUpdate();
        addListenerOnButtonCancel(); 
    	addListenerOnTextView();
    	addListenerOnButtonFileDeletion();
        
        //Nachricht von EntryListActivity abrufen
        Intent i = getIntent();
    	String s = i.getStringExtra("booking");
    	
    	//Booking aus JSON-String erstellen
    	Gson gson = new Gson();
    	Booking booking = gson.fromJson(s, Booking.class);      	
    	fileName = booking.fileName;
    	
    	//Buchung anzeigen, Views füllen
    	showItem(booking);
    }
	
	/**
	 * Aufruf beim Drücken der "Zurück"-Taste
	 */
	@Override
	public void onBackPressed() {
	   Log.d(TAG, "onBackPressed Called");
	   close();
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
        Spinner aSpinner = (Spinner)findViewById(R.id.spinnerAccount2);
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
        Spinner cSpinner = (Spinner)findViewById(R.id.spinnerCategory2);
        //Spinner mit Daten füllen
        helper.fillSpinnerWithData(cSpinner, this, categories);  	
	}
	
	//------------------------------SPEICHERN LÖSCHEN---------------------------------------------------
	//--------------------------------------------------------------------------------------------
	

	/**
	 * Dient dem Speichern einer Buchung
	 */
	@SuppressLint("SimpleDateFormat") 
	private void saveItems() {		
		Booking booking = new Booking();
		//alte Datei löschen
		deleteFile(fileName);
		
		//Booking initialisieren
		booking.account = (Account) tvAccount.getSelectedItem();
		booking.category = (Category) tvCategory.getSelectedItem();
		booking.amount = Double.parseDouble(tvAmount.getText().toString());
		booking.reason = tvReason.getText().toString();	
		try {
			booking.date = new SimpleDateFormat("dd-MM-yyyy").parse(tvDate.getText().toString());
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		booking.fileName = fileName;
		
		//Datei erstellen
		FileOutputStream fos = null;
		try {
			fos = openFileOutput(booking.fileName, MODE_PRIVATE);
		} catch (FileNotFoundException e) {
			Log.e(TAG, "JSON", e);
		}
		//Buchung in Datei speichern
		booking.saveBookingIntoFile(booking, fos);
				
		//Activity schließen
		close();
	} 

	/**
	 * Activity schließen und StartTabActivity neu starten
	 */
	private void close() {
		//Nachricht für StartTabActivity erstellen
		Intent refresh = new Intent(this, StartTabActivity.class);
		//Activity starten
		startActivity(refresh);
		//UpdateEntryActivity schließen
		this.finish();
		
	}

//-----------------------------------------------------------------------------------------------
//---------------------------------FILL FIELDS--------------------------------------------------	
	
	/**
	 * Buchung anzeigen, Views füllen
	 * @param booking Buchung
	 */
	@SuppressLint("SimpleDateFormat") 
	private void showItem(Booking booking) {
		declareTextViews();
    	tvReason.setText(booking.reason);
    	String s = new SimpleDateFormat("dd-MM-yyyy").format(booking.date);
    	tvDate.setText(s);
    	tvAmount.setText(String.valueOf(booking.amount));
    	selectSpinnerItem(tvAccount, booking.account.name);
    	selectSpinnerItem(tvCategory, booking.category.name); 
	}
	
	/**
	 * Dient dem Setzen des jeweiligen Spinners auf entsprechendes Element von Booking
	 * @param spinner 	entsprechender Spinner
	 * @param item		Name des entsprechenden Elements von Booking
	 */
	private void selectSpinnerItem(Spinner spinner, String item) {
		//für alle Spinner-Elemente
		for(int i=0; i<spinner.getCount(); i++){
			//Wenn Spinnerelement mit entsprechendem Element von Booking übereinstimmt
    		if (spinner.getItemAtPosition(i).toString().equals(item)){
    			//Spinner auf entsprechendes ELement setzen
    			spinner.setSelection(i);
    		}
    	}
	}
	
	/**
	 * Dient der Deklaration und Initialisierung der Views
	 */
	private void declareTextViews() {
		tvAccount = (Spinner)findViewById(R.id.spinnerAccount2);
		tvCategory = (Spinner)findViewById(R.id.spinnerCategory2);
		tvAmount = (TextView)findViewById(R.id.tbAmount2);
		tvReason = (TextView)findViewById(R.id.tbReason2);
		tvDate = (TextView)findViewById(R.id.tbDate2);
	}

//----------------------------------------------------------------------------------------------
//------------------------------------LISTENER---------------------------------------------------
	
	/**
	 * Listener für den Button "Abbrechen"
	 */
	private void addListenerOnButtonCancel() {
		//Button initialisieren
		Button btDelete = (Button)findViewById(R.id.btCancel);
		//Listener setzen
		btDelete.setOnClickListener(new View.OnClickListener() {			 
			public void onClick(View v) { 	
				//Methode ausführen, wenn auf Button geklickt wurde
				close();			
			}			
		});			
	}
	
	/**
	 * Listener für den Button "Löschen"
	 */
	private void addListenerOnButtonFileDeletion() {
		//Button initialisieren
		Button btDelete = (Button)findViewById(R.id.btFileDeletion);
		//Listener setzen
		btDelete.setOnClickListener(new View.OnClickListener() {			 
			public void onClick(View v) { 	
				//Methode ausführen, wenn auf Button geklickt wurde
				deleteFile(fileName);	
				//Activity schließen
				close();
			}

						
		});			
	}

	/**
	 * Listener für das Datumsfeld
	 */
	private void addListenerOnTextView() {
		//Textfeld initialisieren
		tvDate = (TextView)findViewById(R.id.tbDate2);
		//Listener setzen
        tvDate.setOnClickListener(new View.OnClickListener() {			 
			public void onClick(View v) { 
				//Methode ausführen, wenn auf Textfeld geklickt wurde
				showDialog(DATE_DIALOG_ID); 
			}
		});		
		
	}

	/**
	 * Listener für den Button "Update"
	 */
	private void addListenerOnButtonUpdate() {
		//Button initialisieren
		Button btSave = (Button)findViewById(R.id.btUpdate);
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
	
	
}

