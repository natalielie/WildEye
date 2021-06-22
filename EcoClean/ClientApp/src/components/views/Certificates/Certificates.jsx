import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserManager, WebStorageStateStore } from 'oidc-client';
import authService from '../../api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import CertificatesApi from '../../services/CertificatesApi';
import { useTranslation } from 'react-i18next';
import i18next from "i18next";

import { withTranslation } from "react-i18next";

function CertificateRow(props) {
    const certificate = props.certificate
    const { t, i18n } = useTranslation();


    var dateFormat = require("dateformat");

    /*const token = await authService.getAccessToken();
    let enterprisesFromApi = [];

    fetch(
        'api/client/getAllEnterprises', {
        headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    })
        .then(response => {
            return response.json();
        })
        .then(data => {
            enterprisesFromApi = data.map(enterprise => {
                return { value: enterprise.enterpriseId, display: enterprise.name };
            });
        })
        .catch(error => {
            console.log(error);
        });

    let enterprise = enterprisesFromApi.filter(enterprise => enterprise.enterpriseId == certificate.enterpriseId)
*/
    var enterpr = "";
    if (certificate.enterpriseId == 1) {
        enterpr = "WOG"
    }
    else {
        enterpr = "ShoesOn"
    }
    if (i18next.language == "en") {
        return (

            <tr key={certificate.certificateId.toString()}>
                <td>{enterpr}</td>
                <td>{dateFormat(certificate.certificateDate, "yyyy/mm/dd")}</td>
                <td>
                    <DeleteModal onDelete={() => props.deleteCertificateHandler(
                        certificate.certificateId)} />
                </td>
            </tr>
        )
    }
    else {
        return (

            <tr key={certificate.certificateId.toString()}>
                <td>{enterpr}</td>
                <td>{dateFormat(certificate.certificateDate, "dd-mm-yyyy")}</td>
                <td>
                    <DeleteModal onDelete={() => props.deleteCertificateHandler(
                        certificate.certificateId)} />
                </td>
            </tr>
        )
    }
    }

class Certificates extends Component {

    constructor() {
        super();
        this.state = { certificates: [], username: "" };

    }

    async componentDidMount() {
        var user = await authService.getUser();
        const username = user.preferred_username;
        this.state.username = username;
        document.title = "Certificates";
        if (username == "admin@gmail.com") {
            this.updateCertificatesAdminHandler();
        }
        else {
            this.updateCertificatesHandler(user.sub);

        }
    }

    // for admin
    updateCertificatesAdminHandler = () => CertificatesApi.getAllCertificates(
        certificates => this.setState({ certificates: certificates }));

    addEnterpriseAdminHandler = (certificate) => CertificatesApi.createCertificate(
        certificate, this.updateCertificatesAdminHandler);

    deleteCertificateHandler = (certificateId) => CertificatesApi.deleteCertificateById(
        certificateId, this.updateCertificatesAdminHandler);

    //for user
    updateCertificatesHandler = async () => await CertificatesApi.getAllUsersCertificates(
        certificates => this.setState({ certificates: certificates }));

    addEnterpriseHandler = (certificate) => CertificatesApi.createCertificate(
        certificate, this.updateCertificatesHandler);

    deleteCertificateHandler = (certificateId) => CertificatesApi.deleteCertificateById(
        certificateId, this.updateCertificatesHandler);


    render() {
        const { t } = this.props;
        var isAdmin;
        if (this.state.username == "admin@gmail.com") {
            isAdmin = true;
        }
        else {
            isAdmin = false;
        }
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
                {isAdmin ? (
                    <Button className="btn btn-primary" style={{ marginTop: 20 }}>
                        <Link tag={Link} className="text-dark" to="/certificate-add/" >{t("Add a new Certificate")}</Link>
                    </Button>
                ):(<></>)}
            </div>
        )
    }
}



export default withTranslation()(Certificates);
