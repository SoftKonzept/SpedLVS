using System;

/// <summary>
/// Zusammenfassungsbeschreibung für clsErrorCode
/// </summary>
public class clsErrorCode
{

    /************************************************************************
    * Aufbau Errorcodebeschreibung:
    * Fehlermeldungen:
    * [0-100] -> Datenbankverbindungen / SQL Connection usw
    * 
    * [100-200] -> Userverwaltung
    * 100 = Add() - Fehler beim Eintrag neuer Daten in die Benutzertabelle
    * 101 = Update() - FEhler beim Update des Datensatzes
     * 
     * [200-300] -> Abrufe
     * 100 =  ADD() - Fehler im Eintrag
    * 
    * ***********************************************************************/


    //--Userverwaltung
    public const Int32 const_ErrorCode_100 = 100;
    public const Int32 const_ErrorCode_101 = 101;


    //-- Abrufe
    public const Int32 const_ErrorCode_200 = 200;
    public const Int32 const_ErrorCode_201 = 201;
}