import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserManager, WebStorageStateStore } from 'oidc-client';
import authService from '../../api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import SmartDeviceApi from '../../services/CertificatesApi';
import { useTranslation } from 'react-i18next';


import { withTranslation } from "react-i18next";

function CertificateRow(props) {
    const certificate = props.certificate
    //const enterpriseLink = `/certificate/${enterprise.enterpriseId}`
    const { t, i18n } = useTranslation();

        return (
            <tr key={certificate.certificateId.toString()}>
                <th scope="row">{certificate.certificateId}</th>
                <td>{certificate.enterpriseId}</td>
                <td>{certificate.certificateDate}</td>
                <td>
                    <DeleteModal onDelete={() => props.deleteCertificateHandler(
                        certificate.certificateeId)} />
                </td>
            </tr>
        )
    }

class Certificates extends Component {

    constructor() {
        super();
        this.state = { certificates: [] };

    }

    async componentDidMount() {
        var user = await authService.getUser();
        const username = user.preferred_username;
        document.title = "Certificates";
        if (username == "admin@gmail.com") {
            this.updateCertificatesAdminHandler();
        }
        else {
            this.updateCertificatesHandler(user.sub);

        }
    }

    // for admin
    updateCertificatesAdminHandler = () => SmartDeviceApi.getAllCertificates(
        certificates => this.setState({ certificates: certificates }));

    addEnterpriseAdminHandler = (certificate) => SmartDeviceApi.createCertificate(
        certificate, this.updateCertificatesAdminHandler);

    deleteEnterpriseHandler = (certificateId) => SmartDeviceApi.deleteCertificateById(
        certificateId, this.updateCertificatesAdminHandler);

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
                                <i className="fa fa-align-justify"></i> {t("Certificates")}
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
                                        {this.state.certificates.map((certificate, index) =>
                                            <CertificateRow key={index} certificate={certificate} deleteCertificateHandler={this.deleteCertificateHandler} />
                                        )}
                                    </tbody>
                                </Table>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
                <Button class="btn btn-primary" style={{ marginTop: 20 }}>
                    <Link tag={Link} className="text-dark" to="/certificate-add/" >{t("Add a new Certificate")}</Link>
                </Button>
            </div>
        )
    }
}



export default withTranslation()(Certificates);
