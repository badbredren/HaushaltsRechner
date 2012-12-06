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

package de.swproject.hbapp.business;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.Date;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

/**
 * Klasse für Buchungen
 * @author Franziska Staake
 *
 */
public class Booking{	
	
	public Account account;
	public double amount;
	public Date date;
	public String reason;
	public Category category;
	public User user;
	public String fileName;	
	
	/**
	 * Konstruktor
	 */
	public Booking(){
		this.account = new Account();
		this.category = new Category();
		this.user = new User();			
	}
	
	/**
	 * Buchung aus Datei lesen
	 * @param fis Datei
	 * @return Booking
	 */	
    public Booking getBookingFromFile(FileInputStream fis){
    	String tag = "MyActivity";
    	Booking booking = new Booking();
    	
		InputStreamReader isr = null;
		BufferedReader br = null;		
		Gson gson = new Gson();
		
		try {
			isr = new InputStreamReader(fis);
			br = new BufferedReader(isr);
			String fileContent = new String();
			// Datei zeilenweise lesen
			while ((fileContent = br.readLine()) != null) {
				//Booking aus JSON-String erstellen
				booking = gson.fromJson(fileContent, Booking.class);   
			}
		} catch (JsonSyntaxException e) {
			Log.e(tag, "JSON", e);
		} catch (IOException e) {
			Log.e(tag, "IO", e);
		} catch (Throwable t) {
			// FileNotFoundException, IOException
			Log.e(tag, "FileNotFound()", t);
		} finally {
			if (br != null) {
    			try {
    				br.close();
    			} catch (IOException e) {
    				Log.e(tag, "br.close()", e);
    			}
			}
			if (isr != null) {
    			try {
    				isr.close();
    			} catch (IOException e) {
    				Log.e(tag, "isr.close()", e);
    				
    			}
			}
			if (fis != null) {
    			try {
    				fis.close();
    			} catch (IOException e) {
    				Log.e(tag, "fis.close()", e);
    			}
			}
		}
		return booking;
    }

    /**
	 * Dient dem Speichern einer Buchung
	 * @param booking	zu speichernde Buchung
	 * @param fos		Datei
	 */
	public void saveBookingIntoFile(Booking booking, FileOutputStream fos){		
		String tag = "";
		
		//Booking in JSON-String schreiben
		Gson gson = new Gson();
		String jsonString = gson.toJson(booking); 
		
		OutputStreamWriter osw = null;
		try {			
    		osw = new OutputStreamWriter(fos);
    		//JSON-String in Datei schreiben
    		osw.write(jsonString);
		} catch (IOException e) {
			Log.e(tag, "IO", e);
		} catch (Throwable t) {
    		// FileNotFoundException, IOException
    		Log.e(tag, "save()", t);
		} finally {
    		if (osw != null) {		    		
	    		try {
	    			osw.close();
	    		} catch (IOException e) {
	    			Log.e(tag, "osw.close()", e);
	    		}
	    	}
    		if (fos != null) {
	    		try {
	    			fos.close();
	    		} catch (IOException e) {
	    			Log.e(tag, "fos.close()", e);
	    		}
	    	}
    	}
					
	}
}

 
