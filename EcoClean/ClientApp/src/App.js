import React, { Component } from 'react';
import { Route, Switch } from "react-router";
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'

import i18next from "i18next";
import { initReactI18next } from "react-i18next";
import HttpApi from "i18next-http-backend";

import { withTranslation } from 'react-i18next';


import i18n from "i18next";
import Enterprises from "./components/views/Enterprises/Enterprises";
import Enterprise from "./components/views/Enterprises/Enterprise";
//import Pets from "./components/views/Clients/Pets";
//import Assignments from "./components/views/Assignments/Assignments";
//import Appointments from "./components/views/Appointments/Appointments";
//import AddAppointment from "./components/views/Appointments/AddAppointment";
//import Schedule from "./components/views/Schedule/Schedules";

import { NavMenu } from "./components/NavMenu";
import LanguageSwitcher from "./components/LanguageSwitcher";
import AddEnterprise from "./components/views/Enterprises/AddEnterprise";
//import PetMedNotes from "./components/views/PetMedNotes/PetMedNotes";
//import Pet from "./components/views/Clients/Pet";
//import AddPetMedNote from "./components/views/PetMedNotes/AddPetMedNote";


const EnterprisesComponent = withTranslation()(Enterprises);
const HomeComponent = withTranslation()(Home);
//const AddAppComponent = withTranslation()(AddAppointment);
const AddEnterComponent = withTranslation()(AddEnterprise);

export default class App extends Component { 
    static displayName = App.name;
    constructor(props) {
        super(props)
    }

  render () {
    return (
        <Layout>
            <LanguageSwitcher />
            
            <Route path="/enterprise-add/" component={AddEnterComponent} />
            <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
            <Route path="/enterprise/" component={Enterprise} />
            <Switch>
                <Route exact path="/" component={HomeComponent} />
                <Route path="/enterprises" component={EnterprisesComponent} />
               {/* <Route path={`/enterprises/${Enterprise}`} component={Enterprise} />
                 <Route path="/pets" component={Pets} />
                <Route path="/assignments" component={Assignments} />
                <Route path="/appointments" component={Appointments} />
                <Route path="/schedule" component={Schedule} />*/}
                <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
            </Switch>

        </Layout> 
    );
  }
}