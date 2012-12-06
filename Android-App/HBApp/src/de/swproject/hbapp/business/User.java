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

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.UUID;

/**
 * Klasse für Benutzer
 * @author Franziska Staake
 *
 */
public class User{
	public UUID id;
	public String name;	


/**
 * Benutzer des Gerätes bestimmen
 * @return User
 */
	public User getCurrentUser(FileInputStream fis) {
		DataHelper helper = new DataHelper();		
		 
		User[] uClass = new User[100];
	    User[] users = (User[]) helper.readFileContent(fis, uClass);
		
		/*AccountManager am = AccountManager.get(this); // "this" references the current Context
		android.accounts.Account[] accounts = am.getAccountsByType("com.google");
		
		int userCount = 0;
		for(int i = 0; i<=accounts.length; i++){
			for(int j = 0; j<=users.length; j++){
				if(accounts[i].name == users[j].name){
					userCount = j;
				}
			}
		}
		
		return users[j];*/
		
		return users[0];
	}
}
