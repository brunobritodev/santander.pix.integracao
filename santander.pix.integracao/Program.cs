using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    var winLogonKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Messaging", false);
    if (winLogonKey is null || !winLogonKey.GetValueNames().Contains("MessageLimitClient"))
    {
        Console.WriteLine(@"Alterar registry:");
        Console.Write(@"Criar Key ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Messaging");
        Console.ResetColor();
        Console.Write(@"E adicionar uma DWORD MessageLimitClient com valor Hexadecimal: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("0x8000");
        Console.ResetColor();
        return;
    }
}

var certificatePath = @"<your-certificate>.pfx";
var certificatePassword = "<your-password>";

var handler = new HttpClientHandler();

handler.ClientCertificateOptions = ClientCertificateOption.Manual;
handler.ClientCertificates.Add(new X509Certificate2(certificatePath, certificatePassword));

var client = new HttpClient(handler);
client.BaseAddress = new Uri("https://trust-pix-h.santander.com.br/");

var content = new FormUrlEncodedContent(new[]
{
    new KeyValuePair<string, string>("client_id", "<your-client_id>"),
    new KeyValuePair<string, string>("client_secret", "<your-client-secret>"),
});
var result = await client.PostAsync("oauth/token?grant_type=client_credentials", content);
Console.WriteLine(await result.Content.ReadAsStringAsync());