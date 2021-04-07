import React, { Component } from 'react';
import { Route } from 'react-router';
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
//import Professionals from "./components/views/Professionals/Professionals";
//import Pets from "./components/views/Clients/Pets";
//import Assignments from "./components/views/Assignments/Assignments";
//import Appointments from "./components/views/Appointments/Appointments";
//import AddAppointment from "./components/views/Appointments/AddAppointment";
//import Schedule from "./components/views/Schedule/Schedules";

import { NavMenu } from "./components/NavMenu";
import LanguageSwitcher from "./components/LanguageSwitcher";
//import AddAssignment from "./components/views/Assignments/AddAssignment";
//import PetMedNotes from "./components/views/PetMedNotes/PetMedNotes";
//import Pet from "./components/views/Clients/Pet";
//import AddPetMedNote from "./components/views/PetMedNotes/AddPetMedNote";


//const ProfessionalsComponent = withTranslation()(Professionals);
const HomeComponent = withTranslation()(Home);
//const AddAppComponent = withTranslation()(AddAppointment);
//const AddAssComponent = withTranslation()(AddAssignment);

export default class App extends Component { 
    static displayName = App.name;
    constructor(props) {
        super(props)
    }

  render () {
    return (
        <Layout>
            <LanguageSwitcher />
            
                <Route exact path="/" component={HomeComponent} />
                
            <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />


        </Layout> 
    );
  }
}
//<Route path="/petMedNote-add/" component={AddPetMedNote} />
//            <Route path="/assignments-add/" component={AddAssComponent} />
//            <Route path="/appointments-add/" component={AddAppComponent} />
//            <Route path="/medcard/" component={PetMedNotes} />
//            <Route path="/pet/" component={Pet} />

// after switch

//<Route path="/professionals" component={ProfessionalsComponent} />
                //<Route path="/pets" component={Pets} />
                //<Route path="/assignments" component={Assignments} />
                //<Route path="/appointments" component={Appointments} />
                //<Route path="/schedule" component={Schedule} />