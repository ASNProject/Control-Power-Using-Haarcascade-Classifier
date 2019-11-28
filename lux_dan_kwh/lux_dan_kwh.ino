#include<Wire.h>
#include <LiquidCrystal.h>

#define Addr 0x4A

int currentPin = A1;              //Assign CT input
double kilos = 0;
int peakPower = 0;

 LiquidCrystal lcd(8, 9, 4, 5, 6, 7);
 
void setup(){
  Wire.begin();
// Initialise serial communication
Serial.begin(9600);

 lcd.begin(16, 2);              // start the library

Wire.beginTransmission(Addr);
Wire.write(0x02);
Wire.write(0x40);
Wire.endTransmission();
delay(300);
}
void loop(){
  luxmeter();
  kwh();
}

void luxmeter(){
unsigned int data[2];
Wire.beginTransmission(Addr);
Wire.write(0x03);
Wire.endTransmission();
 
// Request 2 bytes of data
Wire.requestFrom(Addr, 2);
 
// Read 2 bytes of data luminance msb, luminance lsb
if (Wire.available() == 2)
{
data[0] = Wire.read();
data[1] = Wire.read();
}
 
// Convert the data to lux
int exponent = (data[0] & 0xF0) >> 4;
int mantissa = ((data[0] & 0x0F) << 4) | (data[1] & 0x0F);
float luminance = pow(2, exponent) * mantissa * 0.045;
 
//Serial.print("Ambient Light luminance :");
//Serial.print(luminance);
//Serial.println(" lux");
//delay(500);

lcd.setCursor(9,1);
lcd.print(luminance);
lcd.setCursor(13,1);
lcd.print("lux");
delay(100);

  Serial.print(luminance);
  Serial.println("  lux");
 // delay(100);

}
void kwh(){
    int current = 0;
  int maxCurrent = 0;
  int minCurrent = 1000;
  for (int i=0 ; i<=200 ; i++)  //Monitors and logs the current input for 200 cycles to determine max and min current
  {
    current = analogRead(currentPin);    //Reads current input
    //Serial.print(current);
    if(current >= maxCurrent)
      maxCurrent = current;
    else if(current <= minCurrent)
      minCurrent = current;
  }
  if (maxCurrent <= 517)
  {
    maxCurrent = 516;
  }
  double RMSCurrent = ((maxCurrent - 516)*0.707)/31.14537445;    //Calculates RMS current based on maximum value ganti disiini
  int RMSPower = 220*RMSCurrent;    //Calculates RMS Power Assuming Voltage 220VAC, change to 110VAC accordingly
  if (RMSPower > peakPower)
  {
    peakPower = RMSPower;
  }
  kilos = kilos + (RMSPower * (2.05/60/60/1000));    //Calculate kilowatt hours used
  delay (2000);
    Serial.print(kilos);
  Serial.print("kWh   ");
  lcd.clear();
  lcd.setCursor(0,0);           // Displays all current data
  lcd.print(RMSCurrent);
  lcd.print("A");
  lcd.setCursor(6,0);
  lcd.print(RMSPower);
  lcd.print("W");
  lcd.setCursor(0,01);
  lcd.print(kilos);
  lcd.print("kWh");
  lcd.setCursor(11,0);
  lcd.print(peakPower);
  lcd.print("W");
}

