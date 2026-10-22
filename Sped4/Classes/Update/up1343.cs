using System;

namespace Sped4.Classes.Update
{
    public class up1343
    {
        public const string const_up1343 = "1343";

        public static string SqlString()
        {
            string sql = string.Empty;
            // Neue Tabelle ReiheGutAssignment erstellen (falls noch nicht vorhanden)
            sql += "IF OBJECT_ID('ReiheGutAssignment', 'U') IS NULL " +
                   "BEGIN " +
                   "  CREATE TABLE [ReiheGutAssignment] ( " +
                   "    [Id] INT PRIMARY KEY IDENTITY(1,1), " +
                   "    [ReiheId] DECIMAL(28,0) NOT NULL, " +
                   "    [GArtId] DECIMAL(28,0) NOT NULL, " +
                   "    CONSTRAINT [FK_ReiheGutAssignment_Reihe] FOREIGN KEY ([ReiheId]) REFERENCES [Reihe]([ID]) ON DELETE CASCADE, " +
                   "    CONSTRAINT [FK_ReiheGutAssignment_GueterArt] FOREIGN KEY ([GArtId]) REFERENCES [GueterArt]([ID]) ON DELETE CASCADE, " +
                   "    CONSTRAINT [UQ_ReiheGutAssignment_ReiheId_GArtId] UNIQUE([ReiheId], [GArtId]) " +
                   "  ); " +
                   "END ";

            return sql;
        }

        public static string SqlStringUpdate_UpdateExistingColumns()
        {
            string sql = string.Empty;

            // Migriere bestehende Daten: Falls Reihen bereits GArtID haben, übertrage diese in die neue Tabelle
            sql += "IF OBJECT_ID('ReiheGutAssignment', 'U') IS NOT NULL " +
                   "BEGIN " +
                   "  INSERT INTO [ReiheGutAssignment] ([ReiheId], [GArtId]) " +
                   "  SELECT [ID], [GArtID] " +
                   "  FROM [Reihe] " +
                   "  WHERE [GArtID] > 0 " +
                   "    AND NOT EXISTS (SELECT 1 FROM [ReiheGutAssignment] WHERE [ReiheId] = [Reihe].[ID] AND [GArtId] = [Reihe].[GArtID]); " +
                   "END ";

            return sql;
        }
    }
}
