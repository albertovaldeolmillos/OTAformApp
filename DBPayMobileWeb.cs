using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Reflection;

namespace OTAformApp
{
    public class DBPayMobileWeb:DBComponent
    {

        public DataSet GetMobileUser(string userId)
        {
            StringBuilder sbSQL = new StringBuilder();

            Log.Info("GetMobileUser SQL: " + "SELECT * FROM MOBILE_USERS mu WHERE MU_ID = " + userId + " AND MU_VALID = 1 AND MU_DELETED = 0");
            sbSQL.AppendFormat("SELECT * FROM MOBILE_USERS mu WHERE MU_ID = {0} AND MU_VALID = 1 AND MU_DELETED = 0", userId);


            return GetDataSet(sbSQL.ToString());
        }

        public DataSet GetMobileUserPlates(string userId)
        {
            StringBuilder sbSQL = new StringBuilder();

            Log.Info("GetMobileUserPlates SQL: " + "SELECT MUP_PLATE FROM MOBILE_USERS_PLATES WHERE MUP_MU_ID = " + userId + " AND MUP_VALID = 1 AND MUP_DELETED = 0 ");
            sbSQL.AppendFormat("SELECT MUP_PLATE FROM MOBILE_USERS_PLATES WHERE MUP_MU_ID = {0} AND MUP_VALID = 1 AND MUP_DELETED = 0 ", userId);

            return GetDataSet(sbSQL.ToString());

        }

        public DataSet GetMobileUserByUserName(string userName)
        {
            StringBuilder sbSQL = new StringBuilder();

            Log.Info("GetMobileUserByUserName SQL: " + "SELECT MU_ID FROM  mobile_users WHERE MU_LOGIN = " + userName);
            sbSQL.AppendFormat("SELECT MU_ID FROM  mobile_users WHERE MU_LOGIN = '{0}' ", userName);

            return GetDataSet(sbSQL.ToString());

        }

        public DataSet GetMobileUserByEmail(string email)
        {
            StringBuilder sbSQL = new StringBuilder();

            Log.Info("GetMobileUserByEmail SQL: " + "SELECT MU_ID FROM  mobile_users WHERE MU_EMAIL = " + email);
            sbSQL.AppendFormat("SELECT MU_ID FROM  mobile_users WHERE MU_EMAIL = '{0}'", email);

            return GetDataSet(sbSQL.ToString());
        }

        public DataSet GetOrder()
        {
            StringBuilder sbSQL = new StringBuilder();

            Log.Info("GetOrder SQL: " + "SELECT SEQ_ORDER.NEXTVAL AS ORDER_ID FROM  DUAL");
            sbSQL.AppendFormat("SELECT SEQ_ORDER.NEXTVAL AS ORDER_ID FROM  DUAL");

            return GetDataSet(sbSQL.ToString());

        }

        public int InsertUser(string name, string surname1, string surname2, string dni, string email, string street,
                               string number, string level, string stair, string door_number, string addr_letter, string addr_postal_code,
                                string addr_city, string province, string addr_country, string telephone1, string telephone2, string login, string password, ref string token)
        {


            OracleTransaction oraTrans = GetTransaction();
            int userId = -1;

                string sSQL = " insert into MOBILE_USERS t (mu_name, mu_surname1, mu_surname2, mu_dni, mu_email, mu_addr_street, mu_addr_number, mu_addr_level, mu_addr_stair, mu_addr_letter, mu_addr_postal_code, mu_addr_city, mu_addr_province, mu_addr_country, mu_mobile_telephone, mu_login, mu_password, mu_version, mu_valid, mu_deleted, mu_num_credit_card_mask, mu_activate_account, mu_door_number, mu_mobile_telephone2)";
                sSQL += " VALUES(INITCAP('" + name + "'),INITCAP('" + surname1 + "'),INITCAP('" + surname2 + "'),UPPER('" + dni + "'),'" + email + "','";
                sSQL += street + "','" + number + "','" + level + "','" + stair + "','";
                sSQL += addr_letter + "','" + addr_postal_code + "','" + addr_city + "','" + province + "','";
                sSQL += addr_country + "',";
                sSQL += "'" + telephone1 + "',";
                sSQL += "'" + login + "','" + password + "', null , 1, 0 , null, 0, '" + door_number + "','" + telephone2 + "') returning mu_id into :ID ";

                Log.Info("InsertUser SQL: " + sSQL);
                userId = ExecuteCommandReturningId(sSQL, oraTrans);

                Guid tokenUSER = System.Guid.NewGuid();
                token = tokenUSER.ToString().Replace("-", "");

                StringBuilder sbSQL2 = new StringBuilder();

                Log.Info("InsertUser SQL: " + "insert into MOBILE_USERS_ACTIVATION (mu_activation_key, mu_id, mu_email_date) " + "VALUES('" + token + "', " + userId.ToString() + ", sysdate)");
                sbSQL2.AppendFormat("insert into MOBILE_USERS_ACTIVATION (mu_activation_key, mu_id, mu_email_date) ");
                sbSQL2.AppendFormat("VALUES( '{0}', {1}, sysdate)", token, userId.ToString());

                ExecuteCommand(sbSQL2.ToString(), oraTrans);

                oraTrans.Commit();

                return userId;


        }

        public int UpdateUser(string userId, string name, string surname1, string surname2, string dni, string email, string street,
                               string number, string level, string stair, string door_number, string addr_letter, string addr_postal_code,
                                string addr_city, string province, string addr_country,string telephone1, string telephone2, string login, string password)
        {
            

            
                string updSQL = " UPDATE MOBILE_USERS mu SET ";
                updSQL += " MU_NAME = '" + name + "', ";
                updSQL += " MU_SURNAME1 = '" + surname1 + "', ";
                updSQL += " MU_SURNAME2 = '" + surname2 + "', ";
                updSQL += " MU_DNI = '" + dni + "', ";
                updSQL += " MU_EMAIL = '" + email + "', ";
                updSQL += " MU_ADDR_STREET = '" + street + "', ";
                updSQL += " MU_ADDR_NUMBER = '" + number + "', ";
                updSQL += " MU_ADDR_LEVEL = '" + level + "', ";
                updSQL += " MU_ADDR_STAIR = '" + stair + "', ";
                updSQL += " MU_DOOR_NUMBER = '" + door_number + "', ";
                updSQL += " MU_ADDR_LETTER = '" + addr_letter + "', ";
                updSQL += " MU_ADDR_POSTAL_CODE = '" + addr_postal_code + "', ";
                updSQL += " MU_ADDR_CITY = '" + addr_city + "', ";
                updSQL += " MU_ADDR_PROVINCE = '" + province + "', ";
                updSQL += " MU_ADDR_COUNTRY = '" + addr_country + "', ";
                updSQL += " MU_MOBILE_TELEPHONE = '" + telephone1 + "', ";
                updSQL += " MU_MOBILE_TELEPHONE2 = '" + telephone2 + "', ";
                updSQL += " MU_LOGIN = '" + login + "', ";
                updSQL += " MU_PASSWORD = '" + password + "' ";
                updSQL += " WHERE MU_ID = " + userId;

                Log.Info("UpdateUser SQL: " + updSQL);
                return ExecuteCommand(updSQL);
        }

        public int InserUserPlate(string userId, string plate)
        {
            StringBuilder sbSQL = new StringBuilder();

            Log.Info("InserUserPlate SQL: " + "insert into mobile_users_plates  (mup_mu_id, mup_plate) values (" + userId + "," + plate + ")");
            sbSQL.AppendFormat("insert into mobile_users_plates  (mup_mu_id, mup_plate) ");
            sbSQL.AppendFormat("values ({0}, '{1}') ", userId, plate);

            return ExecuteCommand(sbSQL.ToString());            

        }

        public int DeleteUserPlate(string userId, string plate)
        {
            StringBuilder sbSQL = new StringBuilder();

            Log.Info("DeleteUserPlate SQL: " + "update mobile_users_plates SET MUP_VALID = 0 , MUP_DELETED = 1 where MUP_MU_ID = '" + userId + "' AND MUP_PLATE = '" + plate + "'");
            sbSQL.AppendFormat("update mobile_users_plates SET MUP_VALID = 0 , MUP_DELETED = 1 where MUP_MU_ID = '{0}' AND MUP_PLATE = '{1}'", userId, plate);

            return ExecuteCommand(sbSQL.ToString());

        }

        public int InsertActivationAccount(string UserId, string token)
        {
            StringBuilder sbSQL = new StringBuilder();

            Log.Info("InsertActivationAccount SQL: " + "insert into MOBILE_USERS_ACTIVATION  (mu_activation_key, mu_id, mu_email_date) values (" + token + "," + UserId + ", sysdate)");
            sbSQL.AppendFormat("insert into MOBILE_USERS_ACTIVATION (mu_activation_key, mu_id, mu_email_date) ");
            sbSQL.AppendFormat("VALUES( '{0}', {1}, sysdate)", token, UserId);
            
            return ExecuteCommand(sbSQL.ToString());
            
        }

        public int InsertOrder(string userId, string amount)
        {
            StringBuilder sbSQL = new StringBuilder();

            Log.Info("InsertOrder SQL: " + "insert into mobile_orders  (mo_id, mo_mu_id, mo_amount) values (seq_order.nextval," + userId + "," + amount + ")");
            sbSQL.AppendFormat("insert into mobile_orders  (mo_id, mo_mu_id, mo_amount) ");
            sbSQL.AppendFormat("values  (seq_order.nextval, {0}, {1}) returning mo_id into :ID", userId, amount);

            return ExecuteCommandReturningId(sbSQL.ToString());                     

        }

        public DataSet GetActivations(string key)
        {

            StringBuilder sbSQL = new StringBuilder();

            Log.Info("GetActivations SQL: " + "select * from mobile_users_activation  where mu_activation_key = '" + key + "' and mu_activation_date is null order by mu_email_date desc");
            sbSQL.AppendFormat("select * from mobile_users_activation  where mu_activation_key = '{0}' and mu_activation_date is null order by mu_email_date desc", key);

            return GetDataSet(sbSQL.ToString());


        }

        public int ActivateAccount(string userId)
        {
            StringBuilder sbSQL = new StringBuilder();
            OracleTransaction oraTrans = GetTransaction();
            int numResults = -1;

            Log.Info("ActivateAccount SQL 1: " + "update mobile_users_activation set mu_activation_date = sysdate where mu_id = " + userId);
            sbSQL.AppendFormat("update mobile_users_activation set mu_activation_date = sysdate where mu_id = {0}", userId);

            numResults = ExecuteCommand(sbSQL.ToString(), oraTrans);

            if (numResults > 0)
            {

                StringBuilder sbSQL2 = new StringBuilder();

                Log.Info("ActivateAccount SQL 2: " + "update mobile_users set mu_activate_account = 1 where mu_id = " + userId);
                sbSQL2.AppendFormat("update mobile_users set mu_activate_account = 1 where mu_id = {0}", userId);

                numResults = ExecuteCommand(sbSQL2.ToString(), oraTrans);

                if (numResults != 1)
                {
                    Log.Error("ActivateAccount SQL 2 result KO");
                    oraTrans.Rollback();
                    numResults = -1;

                }
                else
                {
                    Log.Info("ActivateAccount SQL 2 result OK");
                    oraTrans.Commit();
                }
            }
            else
            {
                Log.Error("ActivateAccount SQL 1 result KO");
                numResults = -1;
                oraTrans.Rollback();
            }

            return numResults;


        }

        public int UpdateOrderStatus(string Ds_Order, string Ds_Date, string Ds_Hour, string Ds_Amount, string Ds_Currency,  
                                     string Ds_MerchantCode, string Ds_Terminal, string Ds_Signature, string Ds_Response, 
                                     string Ds_MerchantData, string Ds_SecurePayment, string Ds_TransactionType, string Ds_Card_Country, string Ds_Authorisation_Code,
                                     string Ds_ConsumerLanguage, string Ds_Card_Type, string strIdUser, string strTokenUser)
        {

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.AppendFormat("update mobile_orders ");
            sbSQL.AppendFormat("set mo_date = '{0}', ", Ds_Date);
            sbSQL.AppendFormat("mo_hora = '{0}', ", Ds_Hour);
            sbSQL.AppendFormat("mo_amount = {0}, ", Ds_Amount);
            sbSQL.AppendFormat("mo_currency = '{0}', ", Ds_Currency);
            sbSQL.AppendFormat("mo_merchant_code = '{0}', ", Ds_MerchantCode);
            sbSQL.AppendFormat("mo_terminal = {0}, ", Ds_Terminal);
            sbSQL.AppendFormat("mo_signature = '{0}', ", Ds_Signature);
            sbSQL.AppendFormat("mo_response = '{0}', ", Ds_Response);
            sbSQL.AppendFormat("mo_merchant_data = '{0}', ", Ds_MerchantCode );
            sbSQL.AppendFormat("mo_secure_payment = {0}, ", Ds_SecurePayment);
            sbSQL.AppendFormat("mo_transaction_type = '{0}', ", Ds_TransactionType);
            sbSQL.AppendFormat("mo_card_country = '{0}', ", Ds_Card_Country);
            sbSQL.AppendFormat("mo_authorisation_code = '{0}', ",Ds_Authorisation_Code);
            sbSQL.AppendFormat("mo_language = '{0}', ", Ds_ConsumerLanguage);
            sbSQL.AppendFormat("mo_card_type = '{0}', ", Ds_Card_Type);
            sbSQL.AppendFormat("mo_token_user_id = {0}, ", strIdUser);
            sbSQL.AppendFormat("mo_token_id = '{0}' ", strTokenUser);
            sbSQL.AppendFormat("where mo_id = {0}", Ds_Order);

            Log.Info("UpdateOrderStatus SQL: " + sbSQL.ToString());
            return ExecuteCommand(sbSQL.ToString());

        }

        public DataSet GetUserOrders(string userId, string period)
        {
            DateTime IniDate;
            DateTime EndDate;  
            StringBuilder sbSQL = new StringBuilder();

            //sbSQL.AppendFormat("select mo_id as Id,'Recarga' as Tipo, to_date(mo_date || ' ' || mo_hora, 'dd/mm/yyyy hh24:mi') as fecha, mo_amount/100 as Importe, PARSE_RESULT(mo_response) as resultado from mobile_orders t where mo_mu_id = {0} and mo_date is not null ", userId);
            sbSQL.AppendFormat("select mo_id as Id,'Recarga' as Tipo, TO_DATE(to_char(mo_timestamp,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi') as fecha, mo_amount/100 as Importe, mo_response as resultado from mobile_orders t where mo_mu_id = {0} and mo_timestamp is not null ", userId);

            switch (period)
            {
                case "Todo":
                    //strFilter = "SELECT * FROM operations WHERE ope_mobi_user_id = " + Session["userID"];
                    break;
                case "Actual":
                    IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    EndDate = IniDate.AddMonths(1).AddDays(-1);
                    //sbSQL.AppendFormat("AND to_date(mo_date, 'dd/mm/yyyy') >= to_date('{0}','dd/mm/yyyy') AND to_date(mo_date, 'dd/mm/yyyy') <= to_date('{1}','dd/mm/yyyy') ", IniDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"));
                    sbSQL.AppendFormat("AND TO_DATE(to_char(mo_timestamp,'dd/mm/yyyy'),'dd/mm/yyyy') >= to_date('{0}','dd/mm/yyyy') AND TO_DATE(to_char(mo_timestamp,'dd/mm/yyyy'),'dd/mm/yyyy') <= to_date('{1}','dd/mm/yyyy') ", IniDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"));
                    break;
                case "Anterior":
                    IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                    EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
                    //sbSQL.AppendFormat("AND to_date(mo_date, 'dd/mm/yyyy') >= to_date('{0}','dd/mm/yyyy') AND to_date(mo_date, 'dd/mm/yyyy') <= to_date('{1}','dd/mm/yyyy') ", IniDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"));
                    sbSQL.AppendFormat("AND TO_DATE(to_char(mo_timestamp,'dd/mm/yyyy'),'dd/mm/yyyy') >= to_date('{0}','dd/mm/yyyy') AND TO_DATE(to_char(mo_timestamp,'dd/mm/yyyy'),'dd/mm/yyyy') <= to_date('{1}','dd/mm/yyyy') ", IniDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"));
                    break;
                case "Ultimos3":
                    IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-2);
                    EndDate = IniDate.AddMonths(3).AddDays(-1);
                    //sbSQL.AppendFormat("AND to_date(mo_date, 'dd/mm/yyyy') >= to_date('{0}','dd/mm/yyyy') AND to_date(mo_date, 'dd/mm/yyyy') <= to_date('{1}','dd/mm/yyyy') ", IniDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"));
                    sbSQL.AppendFormat("AND TO_DATE(to_char(mo_timestamp,'dd/mm/yyyy'),'dd/mm/yyyy') >= to_date('{0}','dd/mm/yyyy') AND TO_DATE(to_char(mo_timestamp,'dd/mm/yyyy'),'dd/mm/yyyy') <= to_date('{1}','dd/mm/yyyy') ", IniDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"));
                    break;
            }
            sbSQL.AppendFormat("union ");
            sbSQL.AppendFormat("select mq.mq_id as id, 'Mantenimiento' as Tipo, to_date(to_char(mq_date,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi') as fecha, mq.mq_qty/100 as Importe, 'Cuota ' || decode(to_number(to_char(mq_date,'mm')),1,'Enero',2,'Febrero',3,'Marzo', 4, 'Abril', 5, 'Mayo', 6, 'Junio', 7, 'Julio', 8, 'Agosto', 9, 'Septiembre', 10, 'Octubre', 11, 'Noviembre', 12,'Diciembre') || to_char(mq_date,'yyyy') as resultado ");
            sbSQL.AppendFormat("from mobile_quotes mq where mq_mu_id = {0} ", userId);

            switch (period)
            {
                case "Todo":
                    //strFilter = "SELECT * FROM operations WHERE ope_mobi_user_id = " + Session["userID"];
                    break;
                case "Actual":
                    IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    EndDate = IniDate.AddMonths(1).AddDays(-1);
                    sbSQL.AppendFormat("AND mq_date >= to_date('{0}','dd/mm/yyyy') AND mq_date <= to_date('{1}','dd/mm/yyyy') ", IniDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"));
                    break;
                case "Anterior":
                    IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                    EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
                    sbSQL.AppendFormat("AND mq_date >= to_date('{0}','dd/mm/yyyy') AND mq_date <= to_date('{1}','dd/mm/yyyy') ", IniDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"));

                    break;
                case "Ultimos3":
                    IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-2);
                    EndDate = IniDate.AddMonths(3).AddDays(-1);
                    sbSQL.AppendFormat("AND mq_date >= to_date('{0}','dd/mm/yyyy') AND mq_date <= to_date('{1}','dd/mm/yyyy') ", IniDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"));

                    break;
            }

            sbSQL.AppendFormat(" order by 1 desc");

            Log.Info("GetUserOrders SQL: " + sbSQL.ToString());

            return GetDataSet(sbSQL.ToString());
        }

        public int SetInitialMessage(string userId, string value)
        {
            StringBuilder sbSQL = new StringBuilder();

            Log.Info("GetUserOrders SQL: " + "update mobile_users set mu_message_show = " + value + " where mu_id = " + userId);
            sbSQL.AppendFormat("update mobile_users set mu_message_show = {0} where mu_id = {1}", value, userId);          

            return ExecuteCommand(sbSQL.ToString());                     

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Release managed resources.
            }

            base.Dispose(disposing);
        }


    }
}
