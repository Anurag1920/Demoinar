using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Webinar.DAL.Model
{
    public class CustCardDB
    {
        #region "constructors"
        private PISchologistDBEntities _entities;
        public CustCardDB()
        {
            _entities = new PISchologistDBEntities();
        }
        #endregion

        #region "Methods"

        /// <summary>
        /// Get Customer Card Info
        /// </summary>
        /// <returns></returns>
        public List<tblCardInfo> GetCustCardList(int customerId)
        {
            var objCustCardList = _entities.tblCardInfoes.Where(m => m.customerid == customerId && m.cardno != "NA").ToList().OrderByDescending(m => m.lastused);
            List<tblCardInfo> CardList = objCustCardList.AsEnumerable().Select(objcardinfo => new tblCardInfo
                                    {
                                        cardno = SecurityManager.DecryptText(objcardinfo.cardno),
                                        cardid = objcardinfo.cardid,
                                        cardtype = SecurityManager.DecryptText(objcardinfo.cardtype),
                                        expmonth = SecurityManager.DecryptText(objcardinfo.expmonth),
                                        expyear = SecurityManager.DecryptText(objcardinfo.expyear),
                                        // cardverificationcode = SecurityManager.DecryptText(objcardinfo.cardverificationcode),
                                        // bankname = objcardinfo.bankname
                                    }).ToList();
            return CardList;
        }


        /// <summary>
        /// Save Customer card Info
        /// </summary>
        /// <param name="customerid"></param>
        /// <param name="cardno"></param>
        /// <param name="cardtype"></param>
        /// <param name="expyear"></param>
        /// <param name="expmonth"></param>
        /// <param name="cardverificationcode"></param>
        /// <returns></returns>
        public bool saveCustomerCard(int customerid, decimal amount, string cardno, string cardtype, string expyear, string expmonth, string cardverificationcode, string bankName)
        {
            try
            {
                var CardInfo = _entities.tblCardInfoes.Where(x => x.customerid == customerid).FirstOrDefault();
                if (CardInfo == null)
                {
                    tblCardInfo _custcard = new tblCardInfo();
                    _custcard.customerid = customerid;
                    _custcard.cardno = (cardno != null) ? SecurityManager.EncryptText(cardno) : null;
                    _custcard.cardtype = (cardtype != null) ? SecurityManager.EncryptText(cardtype) : null;
                    _custcard.expmonth = (expmonth != null) ? SecurityManager.EncryptText(expmonth) : null;
                    _custcard.expyear = (expyear != null) ? SecurityManager.EncryptText(expyear) : null;
                    // _custcard.cardverificationcode = (cardverificationcode != null) ? SecurityManager.EncryptText(cardverificationcode) : null;
                    _custcard.lastused = DateTime.Now;
                    _custcard.amtused = amount;
                    // _custcard.bankname = bankName;
                    _entities.tblCardInfoes.Add(_custcard);
                    _entities.SaveChanges();
                    return true;
                }
                else
                {
                    var updatecardinfo = _entities.tblCardInfoes.Where(x => x.cardid == CardInfo.cardid).FirstOrDefault();
                    updatecardinfo.customerid = customerid;
                    updatecardinfo.cardno = (cardno != null) ? SecurityManager.EncryptText(cardno) : null;
                    updatecardinfo.cardtype = (cardtype != null) ? SecurityManager.EncryptText(cardtype) : null;
                    updatecardinfo.expmonth = (expmonth != null) ? SecurityManager.EncryptText(expmonth) : null;
                    updatecardinfo.expyear = (expyear != null) ? SecurityManager.EncryptText(expyear) : null;
                    //  updatecardinfo.cardverificationcode = (cardverificationcode != null) ? SecurityManager.EncryptText(cardverificationcode) : null;
                    updatecardinfo.lastused = DateTime.Now;
                    // updatecardinfo.bankname = bankName;
                    _entities.Entry(updatecardinfo).State = System.Data.Entity.EntityState.Modified;
                    _entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public tblCardInfo GetCardinfo(int CardId)
        {
            tblCardInfo custcarditems = _entities.tblCardInfoes.Where(x => x.cardid == CardId).FirstOrDefault();
            return custcarditems;
        }

        //public itemmast GetItemAmountInfo(int itemid)
        //{
        //    itemmast custcarditems = _entities.itemmasts.Where(x => x.itemid == itemid).FirstOrDefault();
        //    return custcarditems;
        //}

        public bool UpdateCustCardAmount(int CardId, decimal? Amount)
        {
            try
            {
                var UpdatecustCardInfo = _entities.tblCardInfoes.Where(x => x.cardid == CardId).FirstOrDefault();
                UpdatecustCardInfo.amtused = Amount;
                _entities.Entry(UpdatecustCardInfo).State = System.Data.Entity.EntityState.Modified;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCustCardMain(int CardId)
        {
            try
            {
                var UpdatecustCardInfo = _entities.tblCardInfoes.Where(x => x.cardid == CardId).FirstOrDefault();
                if (UpdatecustCardInfo != null)
                {
                    UpdatecustCardInfo.lastused = DateTime.Now;
                    _entities.Entry(UpdatecustCardInfo).State = System.Data.Entity.EntityState.Modified;
                    _entities.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AddNewCard(int customerid, string cardno, string cardtype, string expyear, string expmonth, string cardverificationcode, string bankName)
        {
            string result = string.Empty;
            try
            {

                string Decyptcardno = (cardno != null) ? SecurityManager.EncryptText(cardno) : null;
                bool isExist = _entities.tblCardInfoes.ToList().Any(x => x.cardno == Decyptcardno && x.customerid == customerid);
                if (!isExist)
                {
                    tblCardInfo _custcard = new tblCardInfo();
                    _custcard.customerid = customerid;
                    _custcard.cardno = (cardno != null) ? SecurityManager.EncryptText(cardno) : null;
                    _custcard.cardtype = (cardtype != null) ? SecurityManager.EncryptText(cardtype) : null;
                    _custcard.expmonth = (expmonth != null) ? SecurityManager.EncryptText(expmonth) : null;
                    _custcard.expyear = (expyear != null) ? SecurityManager.EncryptText(expyear) : null;
                    // _custcard.cardverificationcode = (cardverificationcode != null) ? SecurityManager.EncryptText(cardverificationcode) : null;
                    _custcard.lastused = DateTime.Now;
                    // _custcard.bankname = bankName;
                    _entities.tblCardInfoes.Add(_custcard);
                    _entities.SaveChanges();
                    result = "1";
                }
                else
                {
                    result = "2";
                }
            }
            catch (Exception ex)
            {
                result = "3";
            }
            return result;
        }

        public bool UpdateCardinfobyCardId(int cardId, string cardno, string cardtype, string expyear, string expmonth, string cardverificationcode, string bankName)
        {
            var updatecardinfo = _entities.tblCardInfoes.Where(x => x.cardid == cardId).FirstOrDefault();
            updatecardinfo.cardno = (cardno != null) ? SecurityManager.EncryptText(cardno) : null;
            updatecardinfo.cardtype = (cardtype != null) ? SecurityManager.EncryptText(cardtype) : null;
            updatecardinfo.expmonth = (expmonth != null) ? SecurityManager.EncryptText(expmonth) : null;
            updatecardinfo.expyear = (expyear != null) ? SecurityManager.EncryptText(expyear) : null;
            //  updatecardinfo.cardverificationcode = (cardverificationcode != null) ? SecurityManager.EncryptText(cardverificationcode) : null;
            updatecardinfo.lastused = DateTime.Now;
            // updatecardinfo.bankname = bankName;
            _entities.Entry(updatecardinfo).State = System.Data.Entity.EntityState.Modified;
            _entities.SaveChanges();
            return true;
        }
    }

        #endregion



    public class CustCardVM
    {
        public int cardid { get; set; }
        public Nullable<int> customerid { get; set; }
        public string cardno { get; set; }
        public string cardtype { get; set; }
        public string expyear { get; set; }
        public string expmonth { get; set; }
        public string expday { get; set; }
        public Nullable<System.DateTime> expdate { get; set; }
        public Nullable<System.DateTime> lastused { get; set; }
        public Nullable<decimal> amtused { get; set; }
        public Nullable<decimal> amtusedytd { get; set; }
        public string cardverificationcode { get; set; }
        public string bankname { get; set; }
        //   public string cardid { get; set; }
        // public string cardNumbers { get; set; }

        public List<tblCardInfo> CustCardList { get; set; }
        public SelectList Countrylist { get; set; }
        public SelectList amountlist { get; set; }
        public string amount { get; set; }
    }

}
