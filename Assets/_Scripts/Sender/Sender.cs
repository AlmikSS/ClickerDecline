using System;
using System.Net.Http;
using System.Text;
using UnityEngine;

public class Sender
{
    public HttpResponseMessage SendRegisterPost(PostData postData)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://www.api.bombbar.ru");
        client.DefaultRequestHeaders.Add("Authorization", "Bearer bddba698214e6a4de9ae38bce45435fd");

        var json = JsonUtility.ToJson(new { 
            system_user_phone_no_code = postData.PhoneNoCode,
            system_user_phone_code = postData.PhoneCode,
            system_user_email = postData.Email,
            system_user_password = postData.Password });
        
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
        
        return response;
    }

    public void SendBonusPost(PostData postData)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://www.api.bombbar.ru");
        client.DefaultRequestHeaders.Add("Authorization", "Bearer bddba698214e6a4de9ae38bce45435fd");

        var json = JsonUtility.ToJson(new { 
            system_user_phone_no_code = postData.PhoneNoCode,
            system_user_phone_code = postData.PhoneCode,
            system_user_id = postData.Id,
            system_user_bonus_point_count = postData.BonusCount,
            system_user_password = postData.Password });
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync("/webapp/v1/system/create_system_user_bonus_point", content).Result;

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