import React, { Component } from 'react';
import { Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
import SmartDeviceApi from '../../services/SmartDeviceApi';

class SmartDeviceData extends Component {

    constructor() {
        super();

        this.state = { enterprise: {} };
        
        this.getEnterpriseHandler = this.getEnterpriseHandler.bind(this);
    }

    componentDidMount() {
        document.title = "Enterprise";
        this.getEnterpriseHandler(this.props.match.params.enterpriseId);
    }

    getEnterpriseHandler = (enterpriseId) => SmartDeviceApi.getMyEnterprise(
        enterpriseId, enterprise => this.setState({ enterprise: enterprise }));


    render() {

        return (
            <div className="animated fadeIn">
                <Row>
                    <Col lg={6}>
                        <Card>
                            <CardHeader>
                                <strong><i className="icon-info pr-1"></i>Enterprise id: {this.props.match.params.enterpriseId}</strong>
                            </CardHeader>
                            <CardBody>
                                <Table responsive striped hover>
                                    <tbody>
                                        
                                        <tr>
                                            <td>{`¹`}</td>
                                            <td><strong>{this.state.enterprise.enterpriseId}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Name:`}</td>
                                            <td><strong>{this.state.enterprise.name}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Kind:`}</td>
                                            <td><strong>{this.state.enterprise.kind}</strong></td>
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

export default SmartDeviceData;
