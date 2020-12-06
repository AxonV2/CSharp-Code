package com.example.kotlinapp

import android.os.Bundle
import android.view.View
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.activity_exe2.*
import kotlinx.android.synthetic.main.activity_exe3.*
import kotlinx.android.synthetic.main.activity_exe3.editTextNum1
import kotlinx.android.synthetic.main.activity_exe3.editTextNum2
import kotlinx.android.synthetic.main.activity_exe3.textViewResultado

class exe3 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_exe3)
    }

fun sub(): Double{
    val n1 = editTextNum1.text.toString().toDouble()
    val n2 = editTextNum2.text.toString().toDouble()
    return n1 - n2

}

fun clickCalc(view: View){
    //KOTLIN CODE EXAMPLE

    textViewResultado.text = spinner.selectedItem.toString();

    //
    val num1 = editTextNum1.text.toString().toDouble();
    val num2 = editTextNum2.text.toString().toDouble();

    when (spinner.selectedItem.toString()){
        "Soma" -> textViewResultado.text = (num1 + num2).toString();
        "Subtração" -> textViewResultado.text = sub().toString();
        "Multiplicação" -> textViewResultado.text = (num1 * num2).toString();
        "Divisão" -> textViewResultado.text = (num1 / num2).toString();
    }
}



}