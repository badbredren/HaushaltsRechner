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

import android.app.Activity;
import android.app.TabActivity;
import android.os.Bundle;
import android.content.Intent;
import android.widget.TabHost;
import android.widget.TabHost.TabSpec;

/**
 * Diese Activity ist die Start-Activity
 * beinhaltet Tabs mit den andern Activities
 * @author Franziska Staake
 *
 */
public class StartTabActivity extends TabActivity{
	
	/**
	 * Methode welche beim Werfen des onCreate-Events ausgef�hrt wird
	 * @param	savedInstanceState	Bundle, welches den Status der Activity enth�lt
	 */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
      //Bestimmen des Layouts
        setContentView(R.layout.start_tab_activity);
 
        TabHost tabHost = getTabHost();
       
        // Tab f�r neue Eintr�ge
        TabSpec newEntrySpec = createTab(
        		tabHost, "NewEntry", "Neuer Eintrag", 
        		new NewEntryActivity(), R.drawable.icon_new_entry_activity);
 
        // Tab f�r Listenansicht
        TabSpec listSpec = createTab(
        		tabHost, "List", "Liste", new EntryListActivity(), 
        		R.drawable.icon_list_activity);
        
        // Tabs an TabHost binden
        tabHost.addTab(newEntrySpec);
        tabHost.addTab(listSpec);
        
        
      	//tabHost.setCurrentTab(2);      	
    }
    
    /**
     * Dient dem Erstellen von Tabs
     * @param tabHost 	kann Tabs beinhalten
     * @param tabTitel	Titel des Tabs	
     * @param name		Name des Tabs
     * @param activity	Activit, die durch Tabauswahl augerufen werden soll
     * @param icon		Icon des Tabs
     * @return Tab
     */
    private TabSpec createTab(TabHost tabHost, String tabTitel, String name, Activity activity, int icon){
    	// Tab f�r Listenansicht
    	TabSpec spec = tabHost.newTabSpec(name);
        // Titel und Icon f�r Tab
        spec.setIndicator(tabTitel, getResources().getDrawable(icon));
        //Nachricht f�r EntryListActivity
        Intent intent = new Intent(this, activity.getClass());
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
        // Activity an Tab heften
        spec.setContent(intent);    	
    	
    	return spec;
    }
    
    
   
                
    


}
