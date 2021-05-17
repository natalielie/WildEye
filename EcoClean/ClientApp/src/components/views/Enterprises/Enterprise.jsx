import React, { Component } from 'react';
import { Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
import EnterpriseApi from '../../services/EnterpriseApi';

import { withTranslation } from "react-i18next";

class Enterprise extends Component {

    constructor() {
        super();

        this.state = { enterprise: {} };
        
        this.getEnterpriseHandler = this.getEnterpriseHandler.bind(this);
    }

    componentDidMount() {
        document.title = "Enterprise";
        this.getEnterpriseHandler(parseInt(this.props.match.params.id, 10));
    }

    getEnterpriseHandler = async (enterpriseId) => await EnterpriseApi.getMyEnterprise(
        enterpriseId, enterprise => this.setState({ enterprise: enterprise }));


    render() {

        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col lg={6}>
                        <Card>
                            <CardHeader>
                                <strong><i className="icon-info pr-1"></i>{t("My Enterprise")}</strong>
                            </CardHeader>
                            <CardBody>
                                <Table responsive striped hover>
                                    <tbody>
                                        
                                        {/*  <tr>
                                            <td>{`#`}</td>
                                            <td><strong>{this.state.enterprise.enterpriseId}</strong></td>
                                        </tr>*/}
                                        <tr>
                                            <td>{`Name:`}</td>
                                            <td><strong>{this.state.enterprise.name}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Kind:`}</td>
                                            <td><strong>{this.state.enterprise.kind}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Product:`}</td>
                                            <td><strong>{this.state.enterprise.product}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Phone Number:`}</td>
                                            <td><strong>{this.state.enterprise.phoneNumber}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Address:`}</td>
                                            <td><strong>{this.state.enterprise.address}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Rate:`}</td>
                                            <td><strong>{this.state.enterprise.rate}</strong></td>
                                        </tr>
                                        {/* {
                                            patientsTemplate.map(prop =>                                                
                                                <tr key={prop.name}>
                                                    <td>{`${prop.display || utils.capitalize(prop.name)}:`}</td>
                                                    <td><strong>{utils.stringify(this.state.patient[prop.name])}</strong></td>
                                                </tr>
                                            )
                                        } */}
                                    </tbody>
                                </Table>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </div>
        )
    }
}

export default withTranslation()(Enterprise);
