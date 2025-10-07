void setup() {
  Serial.begin(9600);
  Serial.println("Arduino started - Temperature & Humidity Monitor");
}

void loop() {
  // Giả lập giá trị nhiệt độ và độ ẩm
  float temperature = random(200, 350) / 10.0;  // 20.0°C đến 35.0°C
  float humidity = random(400, 900) / 10.0;     // 40.0% đến 90.0%
  
  // Gửi dữ liệu với định dạng rõ ràng, sử dụng ký tự ASCII thông thường
  Serial.print("Temperature: ");
  Serial.print(temperature, 2);  // Hiển thị 2 chữ số thập phân
  Serial.print(" C | ");
  
  Serial.print("Humidity: ");
  Serial.print(humidity, 2);     // Hiển thị 2 chữ số thập phân
  Serial.println(" %");
  
  delay(2000); // Chờ 2 giây
}