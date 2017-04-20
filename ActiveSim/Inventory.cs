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

        private bool AddAssetToInv(int iCitnum, int iAssetnum, int iQuantity)
        {
            
            string sql = "select * from Items where AssetNum = '" + iAssetnum.ToString() + "'";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();

            // Do we have any entries for this assetnum?
            if (reader.HasRows == false)
            {
                // We've got no rows returned! Respond back with this fact and don't add anything.
                Stat(1, "AddInv", "Asset " + iAssetnum + " was not found in the database.", "red");
                return false;
            }

            double Value = 0;
            double Weight = 0;
            string Name = "";
            string sType = "";

            while (reader.Read())
            {
                Name = reader["Name"].ToString();
                Value = Convert.ToDouble(reader["Value"]);
                Weight = Convert.ToDouble(reader["Weight"]);
                sType = reader["Type"].ToString();

            }
            reader.Close();

            // Should check here to see if carry capacity would be reached by adding the item

            // Add code here for looking to see if we have the asset for the specific person in the user inventory table, and if so just update quantity
            sql = "select * from UserInventory where AssetNum = '" + iAssetnum.ToString() + "' and SimProfile = '" + 
                Globals.sSimProfile + "' and Citnum = '" + iCitnum + "'";
            sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            reader = sqlcmd.ExecuteReader();

            if (reader.HasRows == true)
            {

                while (reader.Read())
                {
                    Globals.iCount = Convert.ToInt32(reader["Quantity"]);
                }

                reader.Close();

                iQuantity = iQuantity + Globals.iCount;
                sql = "UPDATE UserInventory SET Quantity = '" + iQuantity + "' where SimProfile = '" + Globals.sSimProfile + "' and AssetNum = '" + 
                    iAssetnum.ToString() + "' and Citnum = '" + iCitnum + "'";
                sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
                sqlcmd.ExecuteNonQuery();

            }
            else
            {
                // If not, then add the item & quantity
                sql = "INSERT INTO UserInventory VALUES ('" + Globals.sSimProfile + "', '" + iCitnum.ToString() + "', '"
                    + iAssetnum.ToString() + "', '" + Name + "', '" + Value.ToString() + "', '" + Weight.ToString()
                    + "', '" + sType + "', '" + iQuantity + "')";
                sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
                sqlcmd.ExecuteNonQuery();
            }
       
            return true;
        }

        private void RemoveAssetFromInv(int iCitnum, int iAssetnum)
        {

        }



    }
}
