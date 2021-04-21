import React, { Component } from 'react';
import { Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
import ReportsApi from '../../services/ReportsApi';
import { withTranslation } from "react-i18next";
import i18next from "i18next";

class Report extends Component {

    constructor() {
        super();

        this.state = { report: {} };
        
        this.getReportHandler = this.getReportHandler.bind(this);
    }

    componentDidMount() {
        document.title = "Report";
        this.getReportHandler(parseInt(this.props.match.params.id, 10));
    }

    getReportHandler = (reportId) => ReportsApi.GetSingleReportOfEnterprise(
        reportId, report => this.setState({ report: report }));

    render() {

        var dateFormat = require("dateformat");

        const { t } = this.props;

        if (i18next.language == "en") {
            return (
                <div className="animated fadeIn">
                    <Row>
                        <Col lg={6}>
                            <Card>
                                <CardHeader>
                                    <strong><i className="icon-info pr-1"></i>{t("Report")}</strong>
                                </CardHeader>
                                <CardBody>
                                    <Table responsive striped hover>
                                        <tbody>
                                            <tr>
                                                <td>{`Name`}</td>
                                                <td><strong>{this.state.report.enterpriseName}</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Air Pollution Substance:`}</td>
                                                <td><strong>{this.state.report.airPollutionSubstance}</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Water Pollution Substance:`}</td>
                                                <td><strong>{this.state.report.waterPollutionSubstance}</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Air Emissions:`}</td>
                                                <td><strong>{this.state.report.airEmissions} ton</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Water Emissions:`}</td>
                                                <td><strong>{this.state.report.waterEmissions} ton</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Tax:`}</td>
                                                <td><strong>{(this.state.report.taxCost / 28.15).toFixed(2)} USD</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Comment:`}</td>
                                                <td><strong>{this.state.report.comment}</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Date:`}</td>
                                                <td><strong>{dateFormat(this.state.report.reportDate, "yyyy/mm/dd")}</strong></td>
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
        else {
            return (
                <div className="animated fadeIn">
                    <Row>
                        <Col lg={6}>
                            <Card>
                                <CardHeader>
                                    <strong><i className="icon-info pr-1"></i>{t("Report")}</strong>
                                </CardHeader>
                                <CardBody>
                                    <Table responsive striped hover>
                                        <tbody>
                                            <tr>
                                                <th scope="col">Назва</th>
                                                <td><strong>{this.state.report.enterpriseName}</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Речовина забруднення повітря:`}</td>
                                                <td><strong>{this.state.report.airPollutionSubstance}</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Речовина забруднення води:`}</td>
                                                <td><strong>{this.state.report.waterPollutionSubstance}</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Об'єм викидів у повітря:`}</td>
                                                <td><strong>{this.state.report.airEmissions} тонн</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Об'єм викидів у воду:`}</td>
                                                <td><strong>{this.state.report.waterEmissions} тонн</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Податок:`}</td>
                                                <td><strong>{this.state.report.taxCost.toFixed(2)} грн</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Коментар:`}</td>
                                                <td><strong>{this.state.report.comment}</strong></td>
                                            </tr>
                                            <tr>
                                                <td>{`Дата:`}</td>
                                                <td><strong>{dateFormat(this.state.report.reportDate, "dd-mm-yyyy")}</strong></td>
                                            </tr>
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
}

export default withTranslation()(Report);
