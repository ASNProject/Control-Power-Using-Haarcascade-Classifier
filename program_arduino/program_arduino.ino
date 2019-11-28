#define BaudRate 9600
#define led 2
int r4 = 9;
int r3 = 10;
int r2 = 11;
int r1 = 12;

char incomingOption;

//double arus_temporary=0.0;
//float adc_Volt, cal_value,temp;
//unsigned long waktu_kalibrasi=0, kalibrasi=600;
//boolean calibration=false;

void setup(){
  pinMode(led, OUTPUT);
  Serial.begin(BaudRate); 
  //Serial.println("Sistem dimulai");
  //Serial.print("waktu kalibrasi :");
  //Serial.println(kalibrasi);

  pinMode(r1, OUTPUT);
  pinMode(r2, OUTPUT);
  pinMode(r3, OUTPUT);
  pinMode(r4, OUTPUT);

  digitalWrite(r1, HIGH);
  digitalWrite(r2, HIGH);
  digitalWrite(r3, HIGH);
  digitalWrite(r4, HIGH);
}

void loop(){
 
  incomingOption = Serial.read();
  //sensorarus();
  switch(incomingOption){
    case '1':
    digitalWrite(r1, LOW);
    break;
    case '0':
    digitalWrite(r1, HIGH);
    break;
    case '2':
    digitalWrite(r2, LOW);
    break;
    case '3':
    digitalWrite(r2, HIGH);
    break;
    case '4':
    digitalWrite(r3, LOW);
    break;
    case '5':
    digitalWrite(r3, HIGH);
    break;
    case '6':
    digitalWrite(r4, LOW);
    break;
    case '7':
    digitalWrite(r4, HIGH);
    break;
    case '8':
    digitalWrite(r1, LOW);
    digitalWrite(r2, LOW);
    digitalWrite(r3, LOW);
    digitalWrite(r4, LOW);
    break;
    case '9':
    digitalWrite(r1, HIGH);
    digitalWrite(r2, HIGH);
    digitalWrite(r3, HIGH);
    digitalWrite(r4, HIGH);
    break;
  }
}

//void sensorarus(){
//temp     = analogRead(A0) * (5.0 / 1023.0); //konversi tegangan analog menjadi digital
//adc_Volt   = abs(temp - 2.50); //mengambil selisih tegangan pada zero point
//adc_Volt  /= 0.185; //Arus dalam A
  //adc_Volt  *= 1000; //merubah Arus A ke mA
 
  //if(waktu_kalibrasi < kalibrasi){ 
   //waktu_kalibrasi++; 
    //Serial.print("Waktu Kalibrasi:");
    //Serial.println(waktu_kalibrasi);
    //arus_temporary += adc_Volt; //penjumlahan arus output sensor
    //calibration = true;
  //}else if(calibration == true){
    //cal_value = arus_temporary/kalibrasi; //pembagian nilai keseluruhan dengan waktu
    //calibration = false;
  //}
 
  //if(calibration == false){
   /// adc_Volt -= cal_value;
    //adc_Volt = abs(adc_Volt);
    //Serial.println("Satuan");
    //Serial.print(" mA :");
    //Serial.println(adc_Volt);
    //adc_Volt /= 1000;
    //Serial.print(" A :");
    //Serial.println(adc_Volt);
    //Serial.println(" ");
    //delay(1000);
  //}
//}

