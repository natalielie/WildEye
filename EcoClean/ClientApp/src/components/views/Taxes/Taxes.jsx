import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserManager, WebStorageStateStore } from 'oidc-client';
import authService from '../../api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import ReportsApi from '../../services/ReportsApi';
import { useTranslation } from 'react-i18next';


import { withTranslation } from "react-i18next";

function ReportRow(props) {
    const report = props.report
    const reportLink = `/report/${report.reportId}`
    const { t, i18n } = useTranslation();

        return (
            <tr key={report.reportId.toString()}>
                <th scope="row"><Link to={reportLink}>{report.reportId}</Link></th>
                <td>{report.enterpriseId}</td>
                <td>{report.comment}</td>
                <td>{report.reportDate}</td>
                <td>{report.taxId}</td>
                <td>
                    <DeleteModal onDelete={() => props.deleteReportHandler(
                        report.reportId)} />
                </td>
            </tr>
        )
    }

class Taxes extends Component {

    constructor() {
        super();
        this.state = { reports: [] };

    }

    async componentDidMount() {
        var user = await authService.getUser();
        const username = user.preferred_username;
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
    updateReportsHandler = () => ReportsApi.getAllReportsOfEnterprise(
        reports => this.setState({ reports: reports }));

    addReportsHandler = (report) => ReportsApi.createReport(
        report, this.updateEnterpriseHandler);

    deleteReportHandler = (reportId) => ReportsApi.deleteReportById(
        reportId, this.updateReportsHandler);


    render() {
        const { t } = this.props;
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
                <Button class="btn btn-primary" style={{ marginTop: 20 }}>
                    <Link tag={Link} className="text-dark" to="/report-add/" >{t("Create a new report")}</Link>
                </Button>
            </div>
        )
    }
}



export default withTranslation()(Taxes);
