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
import Statistics from "./components/views/Statistics/Statistics";
import SetStatistics from "./components/views/Statistics/SetStatistics";
import Reports from "./components/views/Reports/Reports";
import Report from "./components/views/Reports/Report";
import AddReport from "./components/views/Reports/AddReport";
import Certificates from "./components/views/Certificates/Certificates";
import AddCertificate from "./components/views/Certificates/AddCertificate";
import SmartDevice from "./components/views/SmartDevice/SmartDevice";

import { NavMenu } from "./components/NavMenu";
import LanguageSwitcher from "./components/LanguageSwitcher";
import AddEnterprise from "./components/views/Enterprises/AddEnterprise";
import AddData from "./components/views/SmartDevice/AddData";
import GetData from "./components/views/SmartDevice/GetData";


const EnterprisesComponent = withTranslation()(Enterprises);
const EnterpriseComponent = withTranslation()(Enterprise);
const HomeComponent = withTranslation()(Home);
const AddEnterComponent = withTranslation()(AddEnterprise);
const StatComponent = withTranslation()(Statistics);
const ReportsComponent = withTranslation()(Reports);
const ReportComponent = withTranslation()(Report);
const AddReportComponent = withTranslation()(AddReport);
const CertificatesComponent = withTranslation()(Certificates);
const AddCertificateComponent = withTranslation()(AddCertificate);
const SmartDeviceComponent = withTranslation()(SmartDevice);
const AddDataComponent = withTranslation()(AddData);
const GetDataComponent = withTranslation()(GetData);
const SetStatComponent = withTranslation()(SetStatistics);


export default class App extends Component { 
    static displayName = App.name;
    constructor(props) {
        super(props)
    }

  render () {
    return (
        <Layout>
            
            <Route path="/enterprise-add/" component={AddEnterComponent} />
            <Route path="/report-add/" component={AddReportComponent} />
            <Route path="/certificate-add/" component={AddCertificateComponent} />
            <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
            <Route path="/enterprise/:id" component={EnterpriseComponent} />
            <Route path="/report/:id" component={ReportComponent} />
            <Route path="/data-add/" component={AddDataComponent} />
            <Route path="/data-get/" component={GetDataComponent} />
            <Route path="/set-statistics/" component={SetStatComponent} />
            <Switch>
                <Route exact path="/" component={HomeComponent} />
                <Route path="/enterprises" component={EnterprisesComponent} />
                <Route path="/statistics" component={StatComponent} />
                <Route path="/my-reports" component={ReportsComponent} />
                <Route path="/reports" component={ReportsComponent} />
                <Route path="/certificates" component={CertificatesComponent} />
                <Route path="/smart-device" component={SmartDeviceComponent} />
                <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
            </Switch>

        </Layout> 
    );
  }
}