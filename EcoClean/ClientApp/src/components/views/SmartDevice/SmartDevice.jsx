import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserManager, WebStorageStateStore } from 'oidc-client';
import authService from '../../api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import SmartDeviceApi from '../../services/SmartDeviceApi';
import { useTranslation } from 'react-i18next';

import i18next from "i18next";


import { withTranslation } from "react-i18next";

function DataRow(props) {
    const data = props.data

    const { t, i18n } = useTranslation();

    var dateFormat = require("dateformat");

    if (i18next.language == "en") {

        return (
            <tr key={data.smartDeviceDataId.toString()}>
                <th scope="row">{data.smartDeviceDataId}</th>
                <td>{data.enterpriseId}</td>
                <td>{data.airPollution} (ppm)</td>
                <td>{data.waterPollution} (mg/l)</td>
                <td>{dateFormat(data.smartDeviceDataDate, "yyyy/mm/dd")}</td>
            </tr>
        )
    }
    else {
        return (
            <tr key={data.smartDeviceDataId.toString()}>
                <th scope="row">{data.smartDeviceDataId}</th>
                <td>{data.enterpriseId}</td>
                <td>{data.airPollution} (ппм)</td>
                <td>{data.waterPollution} (мг/л)</td>
                <td>{dateFormat(data.smartDeviceDataDate, "dd-mm-yyyy")}</td>
            </tr>
        )
    }
    }

class SmartDevice extends Component {

    constructor() {
        super();
        this.state = { data: [] };

    }

    async componentDidMount() {
        var user = await authService.getUser();
        const username = user.preferred_username;
        document.title = "Smart Device";
        if (username == "admin@gmail.com") {
            this.updateSmartDeviceAdminHandler();
        }
        else {
            this.updateSmartDeviceHandler(user.sub);

        }
    }

    // for admin
    updateSmartDeviceAdminHandler = () => SmartDeviceApi.getAllDataInSystem(
        data => this.setState({ data: data }));

    addSmartDeviceAdminHandler = (data) => SmartDeviceApi.addSmartDeviceData(
        data, this.updateSmartDeviceAdminHandler);

    //for user
    updateSmartDeviceHandler = () => SmartDeviceApi.getAllData(
        data => this.setState({ data: data }));

    addSmartDeviceHandler = (data) => SmartDeviceApi.addSmartDeviceData(
        data, this.updateSmartDeviceHandler);



    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xl={8}>
                        <Card>
                            <CardHeader>
                                <i className="fa fa-align-justify"></i> {t("Smart Device Data")}
                            </CardHeader>
                            <CardBody>
                                <Table responsive hover>
                                    <thead>
                                        <tr>
                                            <th scope="col">№</th>
                                            <th scope="col">{t("Enterprise Name")}</th>
                                            <th scope="col">{t("Air Pollution")}</th>
                                            <th scope="col">{t("Water Pollution")}</th>
                                            <th scope="col">{t("Smart Device Date")}</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.data.map((data, index) =>
                                            <DataRow key={index} data={data} />
                                        )}
                                    </tbody>
                                </Table>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
                <Button class="btn btn-primary" style={{ marginTop: 20 }}>
                    <Link tag={Link} className="text-dark" to="/data-add/" >{t("Add new data")}</Link>
                </Button>
                <Button class="btn btn-primary" style={{ marginTop: 20 }}>
                    <Link tag={Link} className="text-dark" to="/data-пуе/" >{t("Get data from your Smart Device")}</Link>
                </Button>
            </div>
        )
    }
}



export default withTranslation()(SmartDevice);
