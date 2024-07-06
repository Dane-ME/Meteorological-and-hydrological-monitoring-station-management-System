using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;

public class WebSocketTest
{
    public static async Task Main(string[] args)
    {
        // Lấy địa chỉ IP và cổng
        IPHostEntry hostEntry = Dns.GetHostEntry("broker.hivemq.com");
        IPAddress ipAddress = hostEntry.AddressList[0]; // Lấy địa chỉ IP đầu tiên
        int port = 8000; // Cổng

        // Tạo URI WebSocket
        Uri uri = new Uri($"ws://{ipAddress}:{port}");

        // Tạo kết nối WebSocket
        using (ClientWebSocket webSocket = new ClientWebSocket())
        {
            try
            {
                await webSocket.ConnectAsync(uri, CancellationToken.None);

                Console.WriteLine("Kết nối thành công!");

                // ... (Thực hiện các thao tác khác)

                // Đóng kết nối
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi kết nối: {ex.Message}");
            }
        }
    }
}