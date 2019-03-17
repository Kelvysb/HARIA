#include <Wiegand.h>
#include <EEPROM.h>

#define LEDVERDE_PIN A0
#define LEDVERMELHO_PIN A1
#define BTN_SET 8
#define BTN_OK 9
#define BTN_PORTA A2
#define RELAY 10
#define ADM 5967025
#define MEM_TRAVA 0
#define MEM_CAD 1

WIEGAND wg;

int trava, posicao, menu, ultok, doorFlag;
long token_atual, ultimotoken;
String serialInput;

void setup() {
  Serial.begin(9600);  
  wg.begin();
  pinMode(LEDVERMELHO_PIN, OUTPUT);
  pinMode(LEDVERDE_PIN, OUTPUT);
  pinMode(RELAY, OUTPUT);
  pinMode(BTN_SET, INPUT);
  pinMode(BTN_PORTA, INPUT);
  pinMode(BTN_OK, INPUT);
  digitalWrite(RELAY, HIGH);
  trava = EEPROM.read(MEM_TRAVA);
  token_atual = 0;
  posicao = 1;
  doorFlag = 0;
  ultok = 0;
  ultimotoken = 0;
  sbLerUltimo();
  sbSetLed();
}

void loop() {
  
  if(wg.available())
  {
    sbLeitura(wg.getCode());  
  }
  
  if(digitalRead(BTN_SET) == LOW){
    sbBotaoSet();
  }

  if(digitalRead(BTN_OK) == LOW){
    sbBotaoOk();
    Serial.println("OPN:BUTTON");
  }

  serialInput = Serial.readString(); 

  if(serialInput != ""){
    if(serialInput.startsWith("SET")){
      sbSet(serialInput);
    }
    if(serialInput.startsWith("OPN")){
      sbBotaoOk();
    }
    if(serialInput.startsWith("LOK")){
      sbLock();
    }
    if(serialInput.startsWith("ULK")){
      sbUnlock();
    }
    if(serialInput.startsWith("LST")){
      sbList();
    }
    if(serialInput.startsWith("STA")){
      sbStatus();
    }
  }
  
  sbPorta();

}

void sbStatus(){

  String strAux;
  int auxDoor;

  strAux = "LOCK:";
  strAux.concat(trava);
  Serial.println(strAux);
  
  strAux = "DOOR:";
  auxDoor = digitalRead(BTN_PORTA);
  if(auxDoor==0){
    auxDoor = 1;
  }else{
    auxDoor = 0;
  }
  strAux.concat(auxDoor);
  Serial.println(strAux);
  
  strAux = "LAST:";
  strAux.concat(ultimotoken);
  
  if(ultok == 1){
    strAux.concat(";OPN");  
  }else{
    strAux.concat(";NEG");
  }
  Serial.println(strAux);
  
}

void sbPorta(){
  String strAux;
  if(doorFlag != digitalRead(BTN_PORTA)){
    strAux = "DOOR:";
    doorFlag = digitalRead(BTN_PORTA);
    if(doorFlag == 0){
      strAux.concat("OPEN");
    }else{
      strAux.concat("CLOSE");
    }
    Serial.println(strAux);
  }
}

void sbBotaoSet(){
  if(trava == 0){
    sbLock();
    Serial.println("LOCK");
  }else{
    sbUnlock();
    Serial.println("UNLOCK");
  }
  delay(500);
}

void sbLock(){
  trava = 1;
  sbSetLed();
  EEPROM.write(MEM_TRAVA, trava);
}

void sbUnlock(){
  trava = 0;
  sbSetLed();
  EEPROM.write(MEM_TRAVA, trava);
}

void sbBotaoOk(){
  sbAbrir();
  delay(500);
}

void sbSetLed(){
  if(trava==0){
    digitalWrite(LEDVERDE_PIN, HIGH);
    digitalWrite(LEDVERMELHO_PIN, LOW);
  }else{
    digitalWrite(LEDVERDE_PIN, LOW);
    digitalWrite(LEDVERMELHO_PIN, HIGH);
  }
  
}

void sbList(){
  String strAux;
  for(int i = 1;i<=20;i++){
    posicao = i;
    sbLer();
    strAux = "POS:";
    strAux.concat(i);
    strAux.concat(";");
    strAux.concat(token_atual);
    Serial.println(strAux);
  }
}

void sbSet(String input){
  String Pos;
  String Code;
  

  Pos = input.substring(4,6);
  Code = input.substring(7);

  posicao = Pos.toInt();

  char auxChar[Code.length()-1];
  
  for(int i = 0;i<Code.length();i++){
      auxChar[i] = (char)Code[i];
  }
  
  token_atual = atol(auxChar);

  sbGravar();
  
  Serial.println("SET:OK");
  
}

void sbLeitura(long codigo){

  String strAux;
  long auxToken = 0;

  strAux = "NEG:";
  strAux.concat(codigo);
  
 
  auxToken = codigo;
  
  if(auxToken > 0){
    ultok = 0;
    ultimotoken = auxToken;
    for(int i = 1;i<=20;i++){
      posicao = i;
      sbLer();
      if(auxToken == token_atual){
         if(trava==0){
            sbAbrir();
            ultok = 1;
            strAux = "OPN:";
            strAux.concat(token_atual);  
         }
         token_atual = auxToken;
        
      }
    }
    sbGravaUltimo();
    token_atual = 0;  
  }  

  Serial.println(strAux);
  delay(500);
  
}

void sbAbrir(){
  for(int i = 0;  i <= 3; i++){
    digitalWrite(RELAY, LOW);
    delay(200);
    digitalWrite(RELAY, HIGH);
    delay(200);
  }
}


void sbGravar(){
  int endereco;
  endereco = MEM_CAD * posicao;
  endereco = endereco * 4;
  EEPROMWritelong(endereco, token_atual);
  delay(500);
}

void sbGravaUltimo(){

  int endereco;

  endereco = MEM_CAD * 21;
  endereco = endereco * 4;
  EEPROMWritelong(endereco, ultimotoken);

  endereco = MEM_CAD * 22;
  endereco = endereco * 4;
  EEPROM.write(endereco, ultok);
   
}


void sbLerUltimo(){

  int endereco;

  endereco = MEM_CAD * 21;
  endereco = endereco * 4;
  ultimotoken = EEPROMReadlong(endereco);

  endereco = MEM_CAD * 22;
  endereco = endereco * 4;
  ultok = EEPROM.read(endereco);
   
}


void sbLer(){

  int endereco;
  
  if(posicao > 20){
    posicao = 1;
  }
  
  endereco = MEM_CAD * posicao;
  endereco = endereco * 4;
    
  token_atual = EEPROMReadlong(endereco);
  
}


void EEPROMWritelong(int address, long value)
{
   //Decomposition from a long to 4 bytes by using bitshift.
   //One = Most significant -> Four = Least significant byte
   byte four = (value & 0xFF);
   byte three = ((value >> 8) & 0xFF);
   byte two = ((value >> 16) & 0xFF);
   byte one = ((value >> 24) & 0xFF);

   //Write the 4 bytes into the eeprom memory.
   EEPROM.write(address, four);
   EEPROM.write(address + 1, three);
   EEPROM.write(address + 2, two);
   EEPROM.write(address + 3, one);
}

long EEPROMReadlong(long address)
{
  //Read the 4 bytes from the eeprom memory.
  long four = EEPROM.read(address);
  long three = EEPROM.read(address + 1);
  long two = EEPROM.read(address + 2);
  long one = EEPROM.read(address + 3);
  
  //Return the recomposed long by using bitshift.
  return ((four << 0) & 0xFF) + ((three << 8) & 0xFFFF) + ((two << 16) & 0xFFFFFF) + ((one << 24) & 0xFFFFFFFF);
}
