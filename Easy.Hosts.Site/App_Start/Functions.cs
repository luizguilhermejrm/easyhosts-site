﻿using Easy.Hosts.Site.Models;
using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Easy.Hosts.Site.App_Start
{
    public class Functions
{
        public static string HashText(string text, string nameHash)
{
            HashAlgorithm algoritmo = HashAlgorithm.Create(nameHash);
            if (algoritmo == null)
            {
                throw new ArgumentException("Nomedehashincorreto", "nomeHash");
            }
            byte[] hash = algoritmo.ComputeHash(Encoding.UTF8.GetBytes(text));
            return Convert.ToBase64String(hash);
        }

        public static string CodeBookigSort()
{
            Random codeBooking = new Random();
            int result = codeBooking.Next(999999);
            return result.ToString();
        }

        public static string SendEmail(string emailRecipient, string subject, string bodymessage)
{
            try
            {
                //Criaoendereçodeemaildoremetente
                MailAddress inemail = new MailAddress("EasyHosts<hostseasy@gmail.com>");

                //Criaoendereçodeemaildodestinatário-->
                MailAddress foremail = new MailAddress(emailRecipient);

                MailMessage message = new MailMessage(inemail, foremail);

                message.IsBodyHtml = true;

                //Assuntodoemail
                message.Subject = subject;

                //Conteúdodoemail
                message.Body = bodymessage;
                //PrioridadeE-mail
                message.Priority = MailPriority.Normal;
                //Criaoobjetoqueenviaoe-mail
                SmtpClient client = new SmtpClient();
                //Enviaoemail
                client.Send(message);
                return "success|E-mailenviadocomsucesso";
            }
            catch { return "error|Erroaoenviare-mail"; }
        }

        public static string Encode(string text)
{
            byte[] stringBase64 = new byte[text.Length];
            stringBase64 = Encoding.UTF8.GetBytes(text);
            string encode = Convert.ToBase64String(stringBase64);
            return encode;
        }

        public static string Decode(string text)
{
            var encode = new UTF8Encoding();
            var utf8Decode = encode.GetDecoder();
            byte[] stringValue = Convert.FromBase64String(text);
            int cont = utf8Decode.GetCharCount(stringValue, 0,
            stringValue.Length);
            char[] decodeChar = new char[cont];
            utf8Decode.GetChars(stringValue, 0, stringValue.Length, decodeChar, 0);
            string result = new String(decodeChar);
            return result;
        }

        public static decimal QuantityDaysBooking(DateTime dateCheckin, DateTime dateCheckout, decimal valueBooking)
{
            int days = dateCheckout.Day - dateCheckin.Day;

            return days * valueBooking;
        }
    }
}