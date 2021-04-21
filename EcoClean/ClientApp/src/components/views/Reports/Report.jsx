import React, { Component } from 'react';
import { Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
import ReportsApi from '../../services/ReportsApi';
import { withTranslation } from "react-i18next";


class Report extends Component {

    constructor() {
        super();

        this.state = { report: {} };
        
        this.getReportHandler = this.getReportHandler.bind(this);
    }

    componentDidMount() {
        document.title = "Enterprise";
        this.getEnterpriseHandler(this.props.match.params.enterpriseId);
    }

    getReportHandler = (reportId) => ReportsApi.GetSingleReportOfEnterprise(
        reportId, report => this.setState({ report: report }));


    render() {

        return (
            <div className="animated fadeIn">
                <Row>
                    <Col lg={6}>
                        <Card>
                            <CardHeader>
                                <strong><i className="icon-info pr-1"></i>Report id: {this.props.match.params.reportId}</strong>
                            </CardHeader>
                            <CardBody>
                                <Table responsive striped hover>
                                    <tbody>
                                        <tr>
                                            <td>{`Name`}</td>
                                            <td><strong>{this.state.enterprise.enterpriseId}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Air Pollution Substance:`}</td>
                                            <td><strong>{this.state.enterprise.airPollutionSubstance}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Water Pollution Substance:`}</td>
                                            <td><strong>{this.state.enterprise.waterPollutionSubstance}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Air Emissions:`}</td>
                                            <td><strong>{this.state.enterprise.airEmissions}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Water Emissions:`}</td>
                                            <td><strong>{this.state.enterprise.waterEmissions}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Tax:`}</td>
                                            <td><strong>{this.state.enterprise.taxCost}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Comment:`}</td>
                                            <td><strong>{this.state.enterprise.comment}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Date:`}</td>
                                            <td><strong>{this.state.enterprise.reportDate}</strong></td>
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

export default withTranslation()(Report);
