﻿using Birko.SuperFaktura.Request.Client;
using Birko.SuperFaktura.Request.Invoice;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Invoice;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class Invoices
    {
        private readonly SuperFaktura superFaktura;

        public Invoices(SuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<Detail> Get(int ID)
        {
            var result = await superFaktura.Get(string.Format("invoices/view/{0}.json", ID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Detail>(result);
        }

        public async Task<PagedResponse> Get(Request.Invoice.Filter filter, bool listInfo = true)
        {
            var result = await superFaktura.Get(string.Format("invoices/index.json{0}", filter.ToParameters(listInfo))).ConfigureAwait(false);
            if (listInfo)
            {
                return superFaktura.DeserializeResult<PagedResponse>(result);
            }
            else
            {
                return new PagedResponse { Items = superFaktura.DeserializeResult<ItemList<ListItem>>(result) };
            }
        }

        public async Task<ResponsePayment> Pay(Request.Invoice.Payment payment)
        {
            var result = await superFaktura.Post("invoice_payments/add/ajax:1/api:1", new { InvoicePayment = payment }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<ResponsePayment>(result);
        }

        public async Task<byte[]> GetPdf(int invoiceId, string token, string language = LanguageType.Slovak)
        {

            if (!LanguageType.Languages.Contains(language))
            {
                language = LanguageType.Slovak;
            }
            var result = await superFaktura.GetByte(string.Format("{0}/invoices/pdf/{1}/token:{2}", language, invoiceId, token)).ConfigureAwait(false);
            //Code below tests if response is a SuperFaktura error response or PDF File
            string testResult = Encoding.UTF8.GetString(result);
            try
            {
                superFaktura.DeserializeResult<Response<ExpandoObject>>(testResult);
            }
            catch (Exceptions.Exception)
            {
                throw;
            }
            catch (Exception)
            {
                if (result == null || result.Length == 0)
                {
                    throw;
                }
                //deserialization failed. it is a pdf file
            }
            return result;
        }

        public async Task<Response<ExpandoObject>> SetInvoiceLanguage(int ID, string language = LanguageType.Slovak)
        {
            if (!LanguageType.Languages.Contains(language))
            {
                language = LanguageType.Slovak;
            }
            // error response fix
            var result = await superFaktura.Get(string.Format("invoices/setinvoicelanguage/{0}/lang:{1}", ID, language)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Response<ExpandoObject>> MarkAsSent(MarkEmail email)
        {
            var result = await superFaktura.Post("invoices/mark_as_sent", new { InvoiceEmail = email }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Response<ResponseEmail>> SendEmail(Request.Invoice.Email email)
        {
            var result = await superFaktura.Post("invoices/send", new { Email = email }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ResponseEmail>>(result);
        }

        public async Task<Response<Detail>> Save(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null, Setting setting = null)
        {
            var result = await superFaktura.Post("/invoices/create", new
            {
                Invoice = invoice,
                InvoiceItem = items,
                Tag = tags,
                Client = client,
                InvoiceSetting = new {
                    settings = (setting != null) ? setting : new Request.Invoice.Setting()
                }
            }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Detail>>(result);
        }

        public async Task<Response<DetailData>> Update(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null)
        {
            var result = await superFaktura.Post("/invoices/Edit", new
            {
                Invoice = invoice,
                InvoiceItem = items,
                Tag = tags,
                Client = client,
            }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<DetailData>>(result);
        }

        public async Task<Response<bool>> DeleteItem(int invoiceID, int itemID)
        {
            var result = await superFaktura.Get(string.Format("invoice_items/delete/{1}/invoice_id:{0}", invoiceID, itemID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<bool>>(result);
        }

        public async Task<Response<ExpandoObject>> Delete(int invoiceID)
        {
            var result = await superFaktura.Get(string.Format("invoices/delete/{0}", invoiceID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Response<DetailBasic>> SendPost(Post post)
        {
            var result = await superFaktura.Post("invoices/post", new { Post = post }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<DetailBasic>>(result);
        }
    }
}
