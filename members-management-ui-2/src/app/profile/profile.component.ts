import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

const GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0/me';
const GROUPS_GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0/me/memberOf';

type ProfileType = {
  givenName?: string,
  surname?: string,
  userPrincipalName?: string,
  id?: string
};

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profile!: ProfileType;
  groups: any;

  constructor(
    private http: HttpClient
  ) { }

  ngOnInit() {
    this.getProfile();
    this.getGroups();
  }

  getProfile() {
    this.http.get(GRAPH_ENDPOINT)
      .subscribe(profile => {
        this.profile = profile;
      });
  }

  getGroups() {
    this.http.get(GROUPS_GRAPH_ENDPOINT)
      .subscribe(groups => {
        this.groups = groups;
      });
  }
}