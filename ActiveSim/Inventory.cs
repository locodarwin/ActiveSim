using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ActiveSim
{
    public partial class Form1
    {

        private bool AddAssetToInv(int iCitnum, int iAssetnum, string sType)
        {
            
            string sql = "select * from Item_" + sType + " where AssetNum = '" + iAssetnum.ToString() + "' and SimProfile = '" + Globals.sSimProfile + "'";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();

            // Do we have any entries for this citnum?
            if (reader.HasRows == false)
            {
                // We've got no rows returned! Respond back with this fact and don't add anything.
                Stat(1, "AddInv", "Asset " + iAssetnum + " was not found in table Item_" + sType, "red");
                return false;
            }

            double Value = 0;
            double Weight = 0;
            string Name = "";

            while (reader.Read())
            {
                Name = reader["Name"].ToString();
                Value = Convert.ToDouble(reader["Value"]);
                Weight = Convert.ToDouble(reader["Weight"]);

            }
            reader.Close();

            // Should check here to see if carry capacity would be reached by adding the item

            // Add the item
            sql = "INSERT INTO UserInventory VALUES ('" + Globals.sSimProfile + "', '" + iCitnum.ToString() + "', '"
                + iAssetnum.ToString() + "', '" + Name + "', '" + Value.ToString() + "', '" + Weight.ToString() 
                + "', '" + sType + "')";
            sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            sqlcmd.ExecuteNonQuery();



            return true;
        }

        private void RemoveAssetFromInv(int iCitnum, int iAssetnum)
        {

        }



    }
}
