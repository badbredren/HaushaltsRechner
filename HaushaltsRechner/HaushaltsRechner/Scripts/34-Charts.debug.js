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
/// <reference path="33-Dialogs.debug.js" />
/// <reference path="32-GUI.debug.js" />
/// <reference path="31-Connections.debug.js" />
/// <reference path="30-Base.debug.js" />
/// <reference path="20-Json.debug.js" />
/// <reference path="15-Localisation.debug.js" />
/// <reference path="10-jquery.maskedinput-1.3.min.debug.js" />
/// <reference path="10-jquery.event.drag.debug.js" />
/// <reference path="10-jquery-maskMoney.debug.js" />
/// <reference path="01-jquery-ui-1.9.2.debug.js" />
/// <reference path="00-jquery-1.8.3.debug.js" />
"use strict";

(function (charts, $, undefined) {
    var doc = document,
    hr = haushaltsRechner,
    client = hr.client,
    con = hr.connections,
    dialogs = client.dialogs;
        
    charts.searchParameter = null;

    charts.report = function (divID, searchParameter) {
        /// <summary>
        /// Creates a pie chart
        /// </summary>
        /// <param name="divID">containerDiv</param>
        /// <param name="searchParameter">reportSearchParameter</param>
        var createDataArray, createChart, callback;

        createDataArray = function (data) {
            var dArray = [], len, i, d, item;

            for (i = 0, len = data.length; i < len; i += 1) {
                d = [];
                item = data[i];

                d.push(item.CategoryName);
                d.push(item.Percentage);
                d.push(item);

                dArray.push(d);
            }

            return dArray;
        };

        createChart = function (dataArray) {
            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: divID,
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false
                },
                title: {
                    text: LocalText.Reporting
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage}%</b>',
                    percentageDecimals: 1
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            color: '#000000',
                            connectorColor: '#000000',
                            formatter: function () {
                                var str = '<b>' + this.point.name + '</b>: ';
                                str += this.percentage.toFixed(2) + ' %';

                                return str;
                            }
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    name: LocalText.CategoryPart,
                    data: dataArray,
                    events: {
                        click: function (event) {
                            if (event && event.point &&
                                event.point.config &&
                                event.point.config.length >= 3) {
                                dialogs.showMovements(event.point.config[2].MovementsAmounts);
                            }
                        }
                    }
                }]
            });
        };

        callback = function (msg) {
            var data = $.parseJSON(msg),
                dataArray = createDataArray(data);

            createChart(dataArray);
        };

        con.handleData(searchParameter, "ReportingService", "GetReport", callback, false);
    };
})(window.haushaltsRechner.client.charts = window.haushaltsRechner.client.charts || {}, jQuery);