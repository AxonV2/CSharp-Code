package com.example.kotlinapp

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.TextView
import kotlinx.android.synthetic.main.activity_exe2.*

class exe2 : AppCompatActivity() {
    
    lateinit var button : Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_exe2)

        button = findViewById<Button>(R.id.button7)
    }

    private var xd = 2.0
    private var F : String = "";



    fun <T,U>GenericTry(Gen : T, Gen2 : U){
        val i : T = Gen
    }

    //Can add
    private var arraynums = mutableListOf(5.0, 12.0, 5.0)

    //Cannot Add
    private var arraynums2 = arrayOf(5.0, 12.0, 5.0)
    private var arraynums4 = listOf(5.0, 12.0, 5.0)

    fun arrayFun(view: View) {

        arraynums2.toMutableList().add(5.0);
        arraynums2.toDoubleArray()

        arraynums.add(16.0)

        GenericTry<Double, Int>(5.0, 2)

        for(Test in arraynums)
            F += "" + Test


        val but : Button = findViewById<Button>(R.id.button7)
        //val but2 = findViewById<Button>(R.id.button7)
        but.text =  F;
        //xd *= 2;
        
    }

    fun clickSoma(view: View){
        val num1 =  editTextNum1.text.toString().toDouble()
        val num2 =  editTextNum2.text.toString().toDouble()
        val res = num1 + num2

        textViewResultado.text = res.toString()
    }

    fun clickSub(view: View){
        val num1 =  editTextNum1.text.toString().toDouble()
        val num2 =  editTextNum2.text.toString().toDouble()
        val res = num1 - num2

        textViewResultado.text = res.toString()
    }

    fun clickMult(view: View){
        val num1 =  editTextNum1.text.toString().toDouble()
        val num2 =  editTextNum2.text.toString().toDouble()
        val res = num1 * num2

        textViewResultado.text = res.toString()
    }

    fun clickDiv(view: View){
        val num1 =  editTextNum1.text.toString().toDouble()
        val num2 =  editTextNum2.text.toString().toDouble()
        val res = num1 / num2

        textViewResultado.text = res.toString()
    }

}