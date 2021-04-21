import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserManager, WebStorageStateStore } from 'oidc-client';
import authService from '../../api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import SmartDeviceApi from '../../services/SmartDeviceApi';
import { useTranslation } from 'react-i18next';


import { withTranslation } from "react-i18next";

function DataRow(props) {
    const data = props.smartDeviceData
    //const enterpriseLink = `/certificate/${enterprise.enterpriseId}`
    const { t, i18n } = useTranslation();

        return (
            <tr key={data.smartDeviceDataId.toString()}>
                <th scope="row">{data.smartDeviceDataId}</th>
                <td>{data.enterpriseId}</td>
                <td>{data.smartDeviceDataDate}</td>
                <td>
                    <DeleteModal onDelete={() => props.deleteCertificateHandler(
                        data.certificateeId)} />
                </td>
            </tr>
        )
    }

class SmartDevice extends Component {

    constructor() {
        super();
        this.state = { smartDevice: [] };

    }

    async componentDidMount() {
        var user = await authService.getUser();
        const username = user.preferred_username;
        document.title = "Certificates";
        if (username == "admin@gmail.com") {
            this.updateSmartDeviceAdminHandler();
        }
        else {
            this.updateCertificatesHandler(user.sub);

        }
    }

    // for admin
    updateSmartDeviceAdminHandler = () => SmartDeviceApi.getAllCertificates(
        certificates => this.setState({ certificates: certificates }));

    addSmartDeviceAdminHandler = (certificate) => SmartDeviceApi.createCertificate(
        certificate, this.updateSmartDeviceAdminHandler);

    deleteEnterpriseHandler = (certificateId) => SmartDeviceApi.deleteCertificateById(
        certificateId, this.updateSmartDeviceAdminHandler);

    //for user
    //updateCertificatesHandler = () => CertificateApi.getAllCertificates(
    //    enterprises => this.setState({ enterprises: enterprises }));

    addEnterpriseHandler = (certificate) => SmartDeviceApi.createCertificate(
        certificate, this.updateCertificatesHandler);

    deleteEnterpriseHandler = (certificateId) => SmartDeviceApi.deleteCertificateById(
        certificateId, this.updateCertificatesHandler);


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
                                            <th scope="col">{t("Certificate Date")}</th>
                                            <th scope="col">{t("Terminate")}</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.smartDevice.map((data, index) =>
                                            <DataRow key={index} data={data} deleteDataHandler={this.deleteDataHandler} />
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
            </div>
        )
    }
}



export default withTranslation()(SmartDevice);
