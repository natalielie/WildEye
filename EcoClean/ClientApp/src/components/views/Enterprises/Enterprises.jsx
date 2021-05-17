import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserManager, WebStorageStateStore } from 'oidc-client';
import authService from '../../api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import EnterpriseApi from '../../services/EnterpriseApi';
import { useTranslation } from 'react-i18next';


import { withTranslation } from "react-i18next";

function EnterpriseRow(props) {
    const enterprise = props.enterprise
    const enterpriseLink = `/enterprise/${enterprise.enterpriseId}`
    const { t, i18n } = useTranslation();

        return (
            <tr key={enterprise.enterpriseId.toString()}>
                <th scope="row"><Link to={enterpriseLink}>{enterprise.name}</Link></th>
                <td>{enterprise.kind}</td>
                <td>
                    <DeleteModal onDelete={() => props.deleteEnterpriseHandler(
                        enterprise.enterpriseId)} />
                </td>
            </tr>
        )
    }

class Enterprises extends Component {

    constructor() {
        super();
        this.state = { enterprises: [] };

    }

    async componentDidMount() {
        var user = await authService.getUser();
        const username = user.preferred_username;
        document.title = "Enterprises";
        if (username == "admin@gmail.com") {
            this.updateEnterprisesAdminHandler();
        }
        else {
            this.updateEnterpriseHandler(user.sub);

        }
    }

    // for admin
    updateEnterprisesAdminHandler = async () => await EnterpriseApi.getAllEnterprisesInSystem(
        enterprises => this.setState({ enterprises: enterprises }));

    addEnterpriseAdminHandler = async (enterprise) => await EnterpriseApi.addEnterprise(
        enterprise, this.updateEnterprisesAdminHandler);

    deleteEnterpriseHandler = async (enterpriseId) => await EnterpriseApi.DeleteEnterpriseById(
        enterpriseId, this.updateEnterprisesAdminHandler);

    //for user
    updateEnterpriseHandler = async () => await EnterpriseApi.getAllEnterprises(
        enterprises => this.setState({ enterprises: enterprises }));

    addEnterpriseHandler = async (enterprise) => await EnterpriseApi.addEnterprise(
        enterprise, this.updateEnterpriseHandler);

    deleteEnterpriseHandler = async (enterpriseId) => await EnterpriseApi.DeleteEnterpriseById(
        enterpriseId, this.updateEnterpriseHandler);


    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xl={8}>
                        <Card>
                            <CardHeader>
                                <i className="fa fa-align-justify"></i> {t("Enterprises")}
                            </CardHeader>
                            <CardBody>
                                <Table responsive hover>
                                    <thead>
                                        <tr>
                                            <th scope="col">{t("Enterprise Name")}</th>
                                            <th scope="col">{t("Kind")}</th>
                                            <th scope="col">{t("Terminate")}</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.enterprises.map((enterprise, index) =>
                                            <EnterpriseRow key={index} enterprise={enterprise} deleteEnterpriseHandler={this.deleteEnterpriseHandler} />
                                        )}
                                    </tbody>
                                </Table>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
                <Button class="btn btn-primary" style={{ marginTop: 20 }}>
                    <Link tag={Link} className="text-dark" to="/enterprise-add/" >{t("Add a new Enterprise")}</Link>
                </Button>
            </div>
        )
    }
}



export default withTranslation()(Enterprises);
