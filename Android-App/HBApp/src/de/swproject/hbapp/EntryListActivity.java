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

    HaushaltsRechner ist Freie Software: Sie k�nnen es unter den Bedingungen
    der GNU General Public License, wie von der Free Software Foundation,
    Version 3 der Lizenz oder (nach Ihrer Option) jeder sp�teren
    ver�ffentlichten Version, weiterverbreiten und/oder modifizieren.

    HaushaltsRechner wird in der Hoffnung, dass es n�tzlich sein wird, aber
    OHNE JEDE GEW�HELEISTUNG, bereitgestellt; sogar ohne die implizite
    Gew�hrleistung der MARKTF�HIGKEIT oder EIGNUNG F�R EINEN BESTIMMTEN ZWECK.
    Siehe die GNU General Public License f�r weitere Details.

    Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
    Programm erhalten haben. Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/

package de.swproject.hbapp;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStreamReader;
import java.text.SimpleDateFormat;
import java.util.ArrayList;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

import de.swproject.hbapp.business.Booking;

import android.os.Bundle;
import android.annotation.SuppressLint;
import android.app.ListActivity;
import android.content.Intent;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;

/**
 * Klasse zum Auflisten aller Buchungseintr�ge
 * @author Franziska Staake
 *
 */
public class EntryListActivity extends ListActivity {

	//Liste der Dateien im Standarddateipfad
	private ArrayList<String> listFileNameOfItems;
	
	/**
	 * Methode welche beim Werfen des onCreate-Events ausgef�hrt wird
	 * @param	savedInstanceState	Bundle, welches den Status der Activity enth�lt
	 */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Bestimmen des Layouts
        setContentView(R.layout.entry_list_activity);
        //Methodenaufrufe
        showEntryList();
    }
    
    //------------------------------LIST------------------------------------------------------
    //----------------------------------------------------------------------------------------
    
    /**
     * Liste aller Buchungen anzeigen
     */
    private void showEntryList() {
    	//Buchungs-Strings lesen
    	ArrayList<String> helpList = readEntries();
    	//Array f�r Buchungs-Strings erstellen
    	String[] entries = new String[readEntries().size()];
    	entries = (String[]) helpList.toArray(entries);
    	//ArrayAdapter erstellen anhand des Buchungs-Arrays und dem Standard-ListLayout
    	final ArrayAdapter<String> listAdapter = 
    			new ArrayAdapter<String>(
    					this,	
    					android.R.layout.simple_list_item_1,
    					entries);
    	//Adapter setzen
    	setListAdapter(listAdapter);
    }
    
    /**
     * Dient dem Lesen der Buchungseintr�ge und R�ckgabe der Buchungen als Strings
     * @return Liste mit Buchungs-Strings
     */
    @SuppressLint("SimpleDateFormat") 
    private ArrayList<String> readEntries() {	
    	//Standardverzeichnis
		File directory = getFilesDir(); 
		//Liste f�r alle Dateien, die eine Buchung enthalten
		ArrayList<String> entryList = new ArrayList<String>();
		
		Booking booking = new Booking();
		
		int i=0;		
		//f�r alle Listeneintr�ge
		for(String s:directory.list()){
			// folgende Dateien �berspringen
			if(s.equals("user.json") || s.equals("accounts.json") || s.equals("categories.json")){
				i++;				
			}else{
				//Buchungen aus Dateiinhalt erstellen
				FileInputStream fis = null;
				try {
					fis = openFileInput(s);
				} catch (FileNotFoundException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				booking = booking.getBookingFromFile(fis);
				//Liste mit Strings f�llen
				entryList.add(
					i + " " 
					+ new SimpleDateFormat("dd-MM-yyyy").format(booking.date) 
					+ " "
					+ booking.reason 
					+ " " 
					+ booking.amount);
				i++;
			}
			
		}
			
		return entryList;
	}
    
    /**
     * Listener f�r die Liste der Buchungen
     * @param list ListeView
     * @param view View
     * @param position Listenelement
     * @param id ID
     */
    protected void onListItemClick(ListView list, View view, int position, long id) {
		super.onListItemClick(list, view, position, id);
		
		Booking booking = new Booking();
		listFileNameOfItems = new ArrayList<String>();
		
		File directory = getFilesDir(); //Verzeichnis
		
		for(String s:directory.list()){				
			listFileNameOfItems.add(s);
		}	
		
		//String des Listenelements auslesen
		String fileContent = ((TextView) view).getText().toString();
		//Substring aus String des Listenelements extrahieren (-> Nr der Datei) 
		//und entsprechenden Dateinamen aus der Liste der Dateien filtern
		String fileName = listFileNameOfItems.get(Integer.parseInt(fileContent.substring(0, 1)));
		//Buchung aus Datei lesen
		FileInputStream fis = null;
		try {
			fis = openFileInput(fileName);
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		booking = booking.getBookingFromFile(fis);
		
		//Nachricht an UpdateEntryActivity erstellen
		Intent i = new Intent(this, UpdateEntryActivity.class);
		//JSON-String aus Booking erstellen
		Gson gson = new Gson();
		String jsonString = gson.toJson(booking); 
		
		//JSON-String f�r UpdateEntryActivity bereitstellen 
		i.putExtra("booking", jsonString);
		//UpdateEntryActivity aufrufen
		startActivity(i);
		//diese Activity schlie�en
		this.finish();
	}

    //-------------------------------------MENU----------------------------------------------
    //----------------------------------------------------------------------------------------
    
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
      * Reaktion auf Men�Auswahl
      * @param item Men�-Auswahl
      * @return true
      */
    public boolean onOptionsItemSelected(MenuItem item) {
    	switch (item.getItemId()) {
    	    //App verlassen
    		case R.id.app_exit: this.finish();    		
    	}
    	return true;
	}
}
