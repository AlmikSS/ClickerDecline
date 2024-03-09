using System;
using System.Net.Http;
using System.Text;
using UnityEngine;

public class Sender
{
    public void SendRegisterPost()
    {
        var postData = new PostData
        {
            Name = "Test",
            Age = 10,
        };

        var client = new HttpClient();
        client.BaseAddress = new Uri("https://www.api.bombbar.ru");
        client.DefaultRequestHeaders.Add("Authorization", "Bearer bddba698214e6a4de9ae38bce45435fd");

        var json = JsonUtility.ToJson(new { system_user_phone_no_code = "9613889462", system_user_phone_code = "+7", system_user_email = "123123", system_user_password = "123123" });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync("/webapp/v1/system/create_system_user", content).Result;

        Debug.Log(response);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            Debug.Log(responseContent);
        }
        else
        {
            Debug.Log("Error: " + response.StatusCode);
        }
    }
}