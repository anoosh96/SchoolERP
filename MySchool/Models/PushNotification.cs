using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySchool.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net;
using System.IO;

namespace MySchool.Models
{
    public class PushNotification
    {
        public static async void sendNotification(string[] tokens, string title, string message, object data)
        {
            var messageInformation = new FcmMessage()

            {



                Title = title,

                Text = message,



                data = data,

                registration_ids = tokens

            };

            //Object to JSON STRUCTURE => using Newtonsoft.Json;

            string jsonMessage = JsonConvert.SerializeObject(messageInformation);


            // Create request to Firebase API
            string FireBasePushNotificationsURL = "https://fcm.googleapis.com/fcm/send";
            var request = new HttpRequestMessage(HttpMethod.Post, FireBasePushNotificationsURL);
            string ServerKey = "AIzaSyDqP6ZFdkQ1kV1ep7zxJp-wuQOw-VAn7vU";
            request.Headers.TryAddWithoutValidation("Authorization", "key =" + ServerKey);

            request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application / json");

            HttpResponseMessage result;

            using (var client = new HttpClient())

            {

                result = await client.SendAsync(request);

            }


        }

        public static void send(string body, string[] devId)
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            string ServerKey = "AAAA4mUUDIE:APA91bHdxbXp7Q5om1YuwPuF7yclp6PiT_Y9GzKL2j0Wqjc4LgvlHLUYagMd3XvPgQUAH9tRnE5avaz5lEtH0BR5BYMmFFNcHFLKdmmHn2K9gDeyfxB1NO3zicD68wMqo6OBuDRT7J6U";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", ServerKey));
            //Sender Id - From firebase project setting  

            String senderId = "972358421633";
            tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                registration_ids = devId,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = body,
                    title = "New Message",
                    badge = 1
                },
            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }


        }

    }
}