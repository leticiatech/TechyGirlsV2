import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
// import BotChat from '../../botchat.js';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent implements OnInit {


  constructor(private router: Router) { }

  ngOnInit() {
    // BotChat.App({
    //     directLine: { secret: '0jbPWuaFzhM.cwA.Cmw.r9PpU71NHz-RZccJoyjhi-cIKHEWkI2ffs6n1-xlqFs' },
    //     user: { id: 'test' },
    //     bot: { id: 'TechyGirlsBot' },
    //     resize: 'detect'
    // }, document.getElementById("bot"));
  }

  cerrarSorteo() {

  }

  sortear() {

  }

}
