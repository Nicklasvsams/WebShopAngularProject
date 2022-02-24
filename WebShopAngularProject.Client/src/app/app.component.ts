import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'WebShopAngularProject-Client';

  ngOnInit(): void {
    window.onclick = (event: Event) => {
      if ((event.target as HTMLButtonElement) != document.getElementById('dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        for (let i = 0; i < dropdowns.length; i++) {
          var openDropdown = dropdowns[i];
          if (openDropdown.classList.contains('show')) {
            openDropdown.classList.remove('show');
          }
        }
      }
    }
  }

  /* When the user clicks on the button,
toggle between hiding and showing the dropdown content */
  dropDownNav() {
    const dropDown = document.getElementById("mydropdown");

    if (dropDown !== null) {

      dropDown.classList.toggle("show");
    }
  }
}