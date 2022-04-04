using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using OTSDecrypt;
using System.Collections;

namespace OTAformApp
{
    public class CUser
    {

        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _surname1;
        public string Surname1
        {
            get { return _surname1; }
            set { _surname1 = value; }
        }

        private string _surname2;
        public string Surname2
        {
            get { return _surname2; }
            set { _surname2 = value; }
        }

        private string _dninif;
        public string DNINIF
        {
            get { return _dninif; }
            set { _dninif = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _addressNum;
        public string AddressNum
        {
            get { return _addressNum; }
            set { _addressNum = value; }
        }

        private string _level;
        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private string _door;
        public string Door
        {
            get { return _door; }
            set { _door = value; }
        }

        private string _stair;
        public string Stair
        {
            get { return _stair; }
            set { _stair = value; }
        }

        private string _letter;
        public string Letter
        {
            get { return _letter; }
            set { _letter = value; }
        }

        private string _postcode;
        public string PostCode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _province;
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        private string _telephone1;
        public string Telephone1
        {
            get { return _telephone1; }
            set { _telephone1 = value; }
        }

        private string _telephone2;
        public string Telephone2
        {
            get { return _telephone2; }
            set { _telephone2 = value; }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _token;
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        private string _funds;
        public string Funds
        {
            set { _funds = value; }
            get { return _funds; }
        }

        private string[] _plates;
        public string[] Plates
        {
            set { _plates = value; }
            get { return _plates; }
        }

        public string _recharge;
        public string Recharge
        {
            set { _recharge = value; }
            get { return _recharge; }
        }


        public string _message;
        public string Message
        {
            set { _message = value; }
            get { return _message; }
        }



        public CUser() { }

        public CUser(string userId)
        {
            SetUserData(userId);
        }

        public void SetUserData(string userId)
        {
            DBPayMobileWeb db = null;
            DataSet dsUserPlates = null;
            DataSet ds = null;

            try
            {
                db = new DBPayMobileWeb();

                ds = db.GetMobileUser(userId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    Id = row["mu_id"].ToString();
                    Name = row["mu_name"].ToString();
                    Surname1 = row["mu_surname1"].ToString();
                    Surname2 = row["mu_surname2"].ToString();
                    DNINIF = row["mu_dni"].ToString();
                    Email = row["mu_email"].ToString();
                    Address = row["mu_addr_street"].ToString();
                    AddressNum = row["mu_addr_number"].ToString();
                    Level = row["mu_addr_level"].ToString();
                    Stair = row["mu_addr_stair"].ToString();
                    Letter = row["mu_addr_letter"].ToString();
                    PostCode = row["mu_addr_postal_code"].ToString();
                    City = row["mu_addr_city"].ToString();
                    Province = row["mu_addr_province"].ToString();
                    Country = row["mu_addr_country"].ToString();
                    Telephone1 = row["mu_mobile_telephone"].ToString();
                    UserName = row["mu_login"].ToString();
                    Recharge = row["mu_recharge_user"].ToString();
                    Message = row["mu_message_show"].ToString();

                    OTSDecrypt.OTSCreditCardDecrypt decrypt = new OTSCreditCardDecrypt();
                    Password = decrypt.Decrypt(Id, row["mu_password"].ToString());

                    Door = row["mu_door_number"].ToString();
                    Telephone2 = row["mu_mobile_telephone2"].ToString();
                    Funds = row["mu_funds"].ToString();

                }

                //Matrículas

                dsUserPlates = db.GetMobileUserPlates(Id);

                Plates = new string[dsUserPlates.Tables[0].Rows.Count];
                int i = 0;
                foreach (DataRow row in dsUserPlates.Tables[0].Rows)
                {
                    Plates[i++] = row["MUP_PLATE"].ToString();
                }


            }
            catch (Exception ex)
            {
                Log.Fatal("SetUserData EXCEPTION: ", ex);
                throw;
            }
            finally
            {
                if (dsUserPlates != null) { dsUserPlates.Dispose(); dsUserPlates = null; }
                if (ds != null) { ds.Dispose(); ds = null; }
                if (db != null) { db.Dispose(); db = null; }
            }


        }

        public void SetUserData(System.Collections.Specialized.NameValueCollection collection)
        {


            Name = collection["name"].ToString().Replace("'", "''");
            Surname1 = collection["surname1"].ToString().Replace("'", "''");
            Surname2 = collection["surname2"].ToString().Replace("'", "''");

            DNINIF = collection["DNINIF"].ToString().Replace(" ", "").Replace("-", "").Replace(".", "");
            Email = collection["Email"].ToString().Replace("'", "''");
            Address = collection["address"].ToString().Replace("'", "''");

            AddressNum = collection["addressNum"].ToString().Replace("'", "''");
            Level = collection["level"].ToString().Replace("'", "''");
            Stair = collection["stair"].ToString().Replace("'", "''");
            Door = collection["door"].ToString().Replace("'", "''");

            Letter = collection["letter"].ToString().Replace("'", "''");
            PostCode = collection["PostCode"].ToString().Replace("'", "''");
            City = collection["city"].ToString().Replace("'", "''");
            Province = collection["province"].ToString().Replace("'", "''");
            Country = collection["country"].ToString().Replace("'", "''");
            Telephone1 = collection["telephone"].ToString().Replace(" ", "").Replace("-", "");
            Telephone2 = collection["telephone2"].ToString().Replace(" ", "").Replace("-", "");
            UserName = collection["user"].ToString().Replace("'", "''");
            Password = collection["password"].ToString().Replace("'", "''");
            char[] separator = new char[] { };
            Plates = collection["plates"].ToString().Split(separator);
            Funds = "0";

        }

        public void Insert()
        {
            DBPayMobileWeb db = null;
            string token = "";

            try
            {
                db = new DBPayMobileWeb();


                int id = db.InsertUser(Name, Surname1, Surname2, DNINIF, Email, Address, AddressNum,
                    Level, Stair, Door, Letter, PostCode, City, Province, Country, Telephone1, Telephone2, UserName, Password, ref token);

                if (id == -1) throw new Exception("Error al insertar sus datos.");

                Id = id.ToString();
                Token = token;


            }
            catch (Exception)
            {
                throw new Exception("Error al modificar sus datos.");
            }
            finally
            {
                if (db != null) { db.Dispose(); db = null; }
            }
        }

        public void Update()
        {
            DBPayMobileWeb db = null;

            try
            {
                db = new DBPayMobileWeb();

                int numRowsAffected = db.UpdateUser(Id, Name, Surname1, Surname2, DNINIF, Email, Address, AddressNum,
                    Level, Stair, Door, Letter, PostCode, City, Province, Country, Telephone1, Telephone2, UserName, Password);

                if (numRowsAffected <= 0) throw new Exception("Error al modificar sus datos.");


            }
            catch (Exception)
            {
                throw new Exception("Error al modificar sus datos.");
            }
            finally
            {
                if (db != null) { db.Dispose(); db = null; }
            }

        }

        public bool EmailExist(string email)
        {

            DBPayMobileWeb db = null;
            bool existEmail = false;
            DataSet ds = null;

            try
            {
                db = new DBPayMobileWeb();

                ds = db.GetMobileUserByEmail(email);

                if (ds.Tables[0].Rows.Count > 0) existEmail = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (ds != null) { ds.Dispose(); ds = null; }
                if (db != null) { db.Dispose(); db = null; }
            }

            return existEmail;



        }

        public bool UserNameExist(string userName)
        {
            DBPayMobileWeb db = null;
            bool existUserName = false;
            DataSet ds = null;

            try
            {
                db = new DBPayMobileWeb();

                ds = db.GetMobileUserByUserName(userName);

                if (ds.Tables[0].Rows.Count > 0) existUserName = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (ds != null) { ds.Dispose(); ds = null; }
                if (db != null) { db.Dispose(); db = null; }
            }

            return existUserName;

        }

        public string GetUserIDActivationPendent(string tokenID)
        {
            DBPayMobileWeb db = null;
            string userId = "-1";
            DataSet ds = null;



            try
            {
                db = new DBPayMobileWeb();

                ds = db.GetActivations(tokenID);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    userId = row["mu_id"].ToString();

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (ds != null) { ds.Dispose(); ds = null; }
                if (db != null) { db.Dispose(); db = null; }
            }

            return userId;


        }

        public bool ActivateAccount()
        {

            DBPayMobileWeb db = null;
            bool activateAccounts = false;

            try
            {
                db = new DBPayMobileWeb();

                int numResults = db.ActivateAccount(Id);

                if (numResults > 0)
                {
                    activateAccounts = true;
                }
            }
            catch (Exception ex)
            {
                Log.Fatal("ActivateAccount EXCEPTION: ", ex);
                throw;
            }
            finally
            {
                if (db != null) { db.Dispose(); db = null; }
            }

            return activateAccounts;
        }

        public string GetOrder()
        {
            DBPayMobileWeb db = null;
            DataSet ds = null;
            string orderId = "-1";

            try
            {
                db = new DBPayMobileWeb();
                ds = db.GetOrder();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    orderId = row["ORDER_ID"].ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (ds != null) { ds.Dispose(); ds = null; }
                if (db != null) { db.Dispose(); db = null; }
            }

            return orderId;
        }


        public string SetUserOrder(string amount)
        {
            DBPayMobileWeb db = null;
            int orderId = -1;

            try
            {
                db = new DBPayMobileWeb();

                orderId = db.InsertOrder(Id, amount);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (db != null) { db.Dispose(); db = null; }
            }

            return orderId.ToString();

        }

        public DataTable GetOrderData(string period)
        {

            DBPayMobileWeb db = null;
            DataSet ds = null;
            DataTable table = new DataTable();

            try
            {
                db = new DBPayMobileWeb();

                ds = db.GetUserOrders(Id, period);
                               
                foreach (DataColumn column in ds.Tables[0].Columns)
                {
                    DataColumn col = new DataColumn();
                    col.ColumnName = (string)column.ColumnName;
                    col.DataType = (Type)column.DataType;
                    table.Columns.Add(col);
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DataRow dataRow = table.NewRow();

                    for (int i = 0; i < row.Table.Columns.Count; i++)
                    {
                        dataRow[row.Table.Columns[i].ColumnName] = row[i].ToString();

                    }

                    table.Rows.Add(dataRow);
                }



               

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (ds != null) { ds.Dispose(); ds = null; }
                if (db != null) { db.Dispose(); db = null; }
            }

            return table;
        }

        public void UpdateOrderStatus(string Ds_Order, string Ds_Date, string Ds_Hour, string Ds_Amount, string Ds_Currency,
                                     string Ds_MerchantCode, string Ds_Terminal, string Ds_Signature, string Ds_Response,
                                     string Ds_MerchantData, string Ds_SecurePayment, string Ds_TransactionType, string Ds_Card_Country, string Ds_Authorisation_Code,
                                     string Ds_ConsumerLanguage, string Ds_Card_Type, string strIdUser, string strTokenUser)
        {

            DBPayMobileWeb db = null;
            int orderId = -1;

            try
            {
                db = new DBPayMobileWeb();

                orderId = db.UpdateOrderStatus(Ds_Order, Ds_Date, Ds_Hour, Ds_Amount, Ds_Currency,
                                      Ds_MerchantCode, Ds_Terminal, Ds_Signature, Ds_Response,
                                      Ds_MerchantData, Ds_SecurePayment, Ds_TransactionType, Ds_Card_Country, Ds_Authorisation_Code,
                                      Ds_ConsumerLanguage, Ds_Card_Type, strIdUser, strTokenUser);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (db != null) { db.Dispose(); db = null; }
            }





        }



        public void RegisterPlates()
        {
            DBPayMobileWeb db = null;
            DataSet dsUserPlates = null;

            try
            {
                ArrayList deletePlates = new ArrayList();

                db = new DBPayMobileWeb();

                dsUserPlates = db.GetMobileUserPlates(Id);

                foreach (DataRow row in dsUserPlates.Tables[0].Rows)
                {
                    deletePlates.Add(row["MUP_PLATE"].ToString());
                }

                ArrayList newPlates = new ArrayList(Plates);
                ArrayList insertPlates = new ArrayList();
             

                // Recorremos las nuevas matriculas
                for (int p = 0; p < newPlates.Count; p++)
                {
                    if (newPlates[p].ToString() != "" && newPlates[p].ToString().Length >= 6)
                    {
                        // Existe matricula en la BD?
                        int indexPlt = deletePlates.IndexOf(newPlates[p]);
                        if (indexPlt >= 0)
                        {
                            // Existe.
                            // Se deja la matricula eliminandola de la Lista Bajas
                            deletePlates.RemoveAt(indexPlt);
                        }
                        else
                        {
                            // Nuevo
                            // Insertamos en la lista Altas
                            insertPlates.Add(newPlates[p]);
                        }
                    }
                }

                //Eliminamos las matrículas que ya no se usan.
                for (int i = 0; i < deletePlates.Count; i++)
                {
                    db.DeleteUserPlate(Id, deletePlates[i].ToString());
                }

                // Nuevas matrículas (se asume que siempre se crea y no se recupera --> preguntar a Celes)           
              
                for (int i = 0; i < insertPlates.Count; i++)
                {
                    db.InserUserPlate(Id, insertPlates[i].ToString());                       
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en la gestión de sus datos sus datos.");
            }
            finally
            {
                if (dsUserPlates != null) { dsUserPlates.Dispose(); dsUserPlates = null; }
                if (db != null) { db.Dispose(); db = null; }
            }
        }

        public void CancelInitialMessage()
        {

            DBPayMobileWeb db = null;


            try
            {
                db = new DBPayMobileWeb();
                db.SetInitialMessage(Id, "1");

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (db != null) { db.Dispose(); db = null; }
            }

        }
    }
}                                    
  