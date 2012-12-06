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
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.UUID;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

import android.app.Activity;
import android.content.Context;
import android.util.Log;
import android.widget.ArrayAdapter;
import android.widget.Spinner;

/**
 * Helper-Klasse, welche verschiedene Methoden bereitstellt
 * @author Franziska Staake
 *
 */
public class DataHelper{
	
	//dient dem Logging
	private static final String TAG = "MyActivity";
	
	/**
	 * entsprechenden Spinner mit Daten füllen
	 * @param spinner Spinner
	 * @param context Activity
	 * @param o		  Objekt, an dem die Klasse geholt wird
	 */
	public void fillSpinnerWithData  (Spinner spinner, Context context, Object[] o){	
    	//ArrayAdapter erstellen anhand des Objekt-Arrays und dem Standard-Spinnerlayout		
    	ArrayAdapter<Object> adapter = new ArrayAdapter<Object>(
    			context,
    			android.R.layout.simple_spinner_item,
    			o);
    	//Layout, welches angezeigt wird, wenn die Auswahlliste erscheint
    	adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
    	//Adapter an den Spinner hängen, Spinner mit Daten füllen
    	spinner.setAdapter(adapter);
	}	

	/**
	 * Objekte aus Datei lesen
	 * @param fis 		Datei
	 * @param klasse 	Objekt, an dem die Klasse geholt wird
	 * @return Objekt-Array
	 */
	public Object[] readFileContent(FileInputStream fis, Object[] klasse){
		InputStreamReader isr = null;
		BufferedReader br = null;
		Gson gson = new Gson();
		Object[] obj = null;
			
		try {
			isr = new InputStreamReader(fis);
			br = new BufferedReader(isr);
			String fileContent = new String();
			String s = new String();
			// Datei zeilenweise lesen
			while ((fileContent = br.readLine()) != null) {
				s += fileContent;
			}
			//Objekte-Array aus JSON-String erstellen
			obj = gson.fromJson(s, klasse.getClass());
			
		} catch (JsonSyntaxException e) {
			Log.e(TAG, "JSON", e);
		} catch (IOException e) {
			Log.e(TAG, "IO", e);
		} catch (Throwable t) {
			// FileNotFoundException, IOException
			Log.e(TAG, "FileNotFound", t);
		} finally {
			if (br != null) {
    			try {
    				br.close();
    			} catch (IOException e) {
    				Log.e(TAG, "br.close()", e);
    			}
			}
			if (isr != null) {
    			try {
    				isr.close();
    			} catch (IOException e) {
    				Log.e(TAG, "isr.close()", e);    				
    			}
			}
			if (fis != null) {
    			try {
    				fis.close();
    			} catch (IOException e) {
    				Log.e(TAG, "fis.close()", e);
    			}
			}
		}
			
		return obj;
		
    }

	

	
}
