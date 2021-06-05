import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserManager, WebStorageStateStore } from 'oidc-client';
import authService from '../../api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import ReportsApi from '../../services/ReportsApi';
import { useTranslation } from 'react-i18next';
import i18next from "i18next";

import { withTranslation } from "react-i18next";


function ReportRow(props) {
    const report = props.report
    const reportLink = `/report/${report.reportId}`
    const { t, i18n } = useTranslation();

    var dateFormat = require("dateformat");
    if (i18next.language == "en") {
        return (
            <tr key={report.reportId.toString()}>
                <th scope="row"><Link to={reportLink}>{report.reportId}</Link></th>
                <td>{report.enterpriseName}</td>
                <td>{report.comment}</td>
                <td>{dateFormat(report.reportDate, "yyyy/mm/dd")}</td>
                <td>{(report.taxCost / 28.15).toFixed(2)} USD</td>
                <td>
                    <DeleteModal onDelete={() => props.deleteReportHandler(
                        report.reportId)} />
                </td>
            </tr>
        )
    }
    else {
        return (
            <tr key={report.reportId.toString()}>
                <th scope="row"><Link to={reportLink}>{report.reportId}</Link></th>
                <td>{report.enterpriseName}</td>
                <td>{report.comment}</td>
                <td>{dateFormat(report.reportDate, "dd-mm-yyyy")}</td>
                <td>{report.taxCost.toFixed(2)} грн</td>
                <td>
                    <DeleteModal onDelete={() => props.deleteReportHandler(
                        report.reportId)} />
                </td>
            </tr>
        )
    }
    }

class Reports extends Component {

    constructor() {
        super();
        this.state = { reports: [], username: "" };

    }

    async componentDidMount() {
        var user = await authService.getUser();
        const username = user.preferred_username;
        this.state.username = username;
        document.title = "Reports";
        if (username == "admin@gmail.com") {
            this.updateReportsAdminHandler();
        }
        else {
            this.updateReportsHandler(user.sub);

        }
    }

    // for admin
    updateReportsAdminHandler = () => ReportsApi.getAllReports(
        reports => this.setState({ reports: reports }));

    addReportsAdminHandler = (report) => ReportsApi.createReport(
        report, this.updateEnterprisesAdminHandler);

    deleteReportHandler = (reportId) => ReportsApi.deleteReportById(
        reportId, this.updateReportsAdminHandler);

    //for user
    updateReportsHandler = async () => await ReportsApi.getAllUsersReports(
        reports => this.setState({ reports: reports }));

    addReportsHandler = (report) => ReportsApi.createReport(
        report, this.updateEnterpriseHandler);

    deleteReportHandler = (reportId) => ReportsApi.deleteReportById(
        reportId, this.updateReportsHandler);


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
                                <i className="fa fa-align-justify"></i> {t("Reports")}
                            </CardHeader>
                            <CardBody>
                                <Table responsive hover>
                                    <thead>
                                        <tr>
                                            <th scope="col">№</th>
                                            <th scope="col">{t("Enterprise Name")}</th>
                                            <th scope="col">{t("Comment")}</th>
                                            <th scope="col">{t("Date")}</th>
                                            <th scope="col">{t("Tax")}</th>
                                            <th scope="col">{t("Terminate")}</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.reports.map((report, index) =>
                                            <ReportRow key={index} report={report} deleteReportHandler={this.deleteReportHandler} />
                                        )}
                                    </tbody>
                                </Table>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
                {isAdmin ? (<></>) : (<Button className="btn btn-primary" style={{ marginTop: 20 }}>
                        <Link tag={Link} className="text-dark" to="/report-add/" >{t("Create a new report")}</Link>
                    </Button>)}
                
            </div>
        )
    }
}



export default withTranslation()(Reports);
