#include <Wire.h>
#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>
#include "SSD1306Wire.h"
#include "fauxmoESP.h"
#include <WiFiClient.h>
#include <ArduinoJson.h>

#ifndef STASSID
#define STASSID "<WIFI_SSID>"
#define STAPSK  "<WIFI_PASSWORD>"
#define APIKEY  "<HARIA_INTEGRATION_API_KEY>"
#define INTEGRATIONSERVER  "<HARIA_INTEGRATION_SERVER_AND_PORT>"
#endif

const int BTN_UP = 0;
const int BTN_DOWN = 2;

const char* ssid     = STASSID;
const char* password = STAPSK;
const String apiKey = APIKEY;
const String integrationServer = INTEGRATIONSERVER;
const int devicesCount = 9;

int page = 0;
int pageSize = 4;
int totalPages = 3;
String connectedIp = "";
String setActuator = "";
String devices[9];
String endpoints[9];

ESP8266WiFiMulti WiFiMulti;
SSD1306Wire display(0x3c, SDA, SCL);
fauxmoESP fauxmo;

void setup() {
  // put your setup code here, to run once:
  String message = "";
  Serial.begin(115200);
  Serial.println();
  Serial.println();
  display.init();  
  display.flipScreenVertically();
  display.setFont(ArialMT_Plain_10);
  display.setTextAlignment(TEXT_ALIGN_CENTER);
  display.drawString(64, 0, "Initializing...");  
  message = "Conecting to " + String(ssid);
  display.drawString(64, 10, message);
  display.display();

  pinMode(BTN_UP, INPUT);
  pinMode(BTN_DOWN, INPUT);
    
  WiFi.mode(WIFI_STA);
  WiFiMulti.addAP(ssid, password);
  while (WiFiMulti.run() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  display.clear();
  display.drawString(64, 0, "Initializing...");
  display.drawString(64, 10, "Connected");
  connectedIp = IpAddress2String(WiFi.localIP());
  message = "IP: " + connectedIp;
  display.drawString(64, 20, message);
  display.display();  

  Serial.println("Starting...");
  
  fauxmo.createServer(true);
  fauxmo.setPort(80);
  fauxmo.enable(true);
  
  //AddDevices  
  fauxmo.addDevice("luz do escritorio");
  fauxmo.addDevice("luz do quarto");
  fauxmo.addDevice("luz do banheiro");  
  fauxmo.addDevice("luz do espelho");
  fauxmo.addDevice("luz da entrada");
  fauxmo.addDevice("luz da cozinha"); 
  fauxmo.addDevice("luz da sala"); 
  fauxmo.addDevice("luz da tv"); 
  fauxmo.addDevice("luz da area de servico");   

  //Associate devices/endpoints  
  devices[0] = "luz do escritorio";
  endpoints[0] = "/set/actuator/AA002/D1";
  devices[1] = "luz do quarto";
  endpoints[1] = "/set/actuator/AA002/D2";
  devices[2] = "luz do banheiro";
  endpoints[2] = "/set/actuator/AA002/D3";
  devices[3] = "luz do espelho";
  endpoints[3] = "/set/actuator/AA002/D5";  
  devices[4] = "luz da entrada";
  endpoints[4] = "/set/actuator/AA001/D0";
  devices[5] = "luz da cozinha";
  endpoints[5] = "/set/actuator/AA001/D1"; 
  devices[6] = "luz da sala";
  endpoints[6] = "/set/actuator/AA001/D2";
  devices[7] = "luz da tv";
  endpoints[7] = "/set/actuator/AA001/D3";
  devices[8] = "luz da area de servico";
  endpoints[8] = "/set/actuator/AA001/D4";
      
  fauxmo.onSetState([](unsigned char device_id, const char * device_name, bool state, unsigned char value) {     
    Serial.printf("[MAIN] Device #%d (%s) state: %s value: %d\n", device_id, device_name, state ? "ON" : "OFF", value);

    for(int index = 0; index < devicesCount; index++){
      if(devices[index] == String(device_name)){
        setActuator = integrationServer;
        setActuator = setActuator + endpoints[index];
        if(state){
          setActuator = setActuator + "?value=true";
        }else{
          setActuator = setActuator + "?value=false";          
        }     
        setActuator = setActuator + "&api_key=" + apiKey;;     
        Serial.println(setActuator.c_str());        
        break;
      }
    }
  });
  
  Serial.print("Started");

  showDevices();
}

void loop() {  
  fauxmo.handle();

  if(setActuator != ""){
      WiFiClient client;
      HTTPClient http;

      if (http.begin(client, setActuator)) {  // HTTP
        Serial.print("HTTP begin");  
        int httpCode = http.GET();
        if (httpCode > 0) {
          Serial.printf("[HTTP] GET... code: %d\n", httpCode);
  
          // file found at server
          if (httpCode == HTTP_CODE_OK || httpCode == HTTP_CODE_MOVED_PERMANENTLY) {
            String payload = http.getString();
            Serial.println(payload);
          }
        } else {
          Serial.printf("[HTTP] GET... failed, error: %s\n", http.errorToString(httpCode).c_str());
        }
        http.end();
      }else{
        Serial.printf("[HTTP] Unable to connect\n");
      }
      
    setActuator = "";
  }

  if(digitalRead(BTN_DOWN) == LOW){
    if(page < totalPages){
      page++;
    }
    showDevices();
    delay(500);
  }else if(digitalRead(BTN_UP) == LOW){
    if(page >  0){
      page--;
    }
    showDevices();
    delay(500);
  }
}

void showDevices(){
  String message = "";
  display.clear();
  connectedIp = IpAddress2String(WiFi.localIP());
  message = "IP: " + connectedIp;
  display.drawString(64, 0, message);
  message = "Devices (" + String(page + 1) + "/" + String(totalPages) + "):";
  display.drawString(64, 10, message);
  int startIndex = (page * pageSize);
  int pos = 0;
  for(int index = startIndex; index < devicesCount; index++){
    display.drawString(64, 20 + pos * 10, devices[index]);
    pos++;
  }  
  display.display();  
}

String IpAddress2String(const IPAddress& ipAddress)
{
  return String(ipAddress[0]) + String(".") +\
  String(ipAddress[1]) + String(".") +\
  String(ipAddress[2]) + String(".") +\
  String(ipAddress[3])  ; 
}
