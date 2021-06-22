import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserManager, WebStorageStateStore } from 'oidc-client';
import authService from '../../api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import StatisticsApi from '../../services/StatisticsApi';
import StatisticsSwitcher from "../../StatisticsSwitcher";
import { useTranslation } from 'react-i18next';


import { withTranslation } from "react-i18next";

const ComboboxSort = (props) => {

    const onChange = e => {
        const { name, value } = e.target;
    }
    const {
        current_stat
    } = 0;

    const sortOptions = [
        {
            label: "↓",
            value: 0,
        },
        {
            label: "↑",
            value: 1,
        },
    ];

    return (
        <>
            <div className="form-group">
                <select value={current_stat} onChange={onChange} id="license_type" name="current_stat">
                    {sortOptions.map((option) => (
                        <option value={option.value}>{option.label}</option>
                    ))}
                </select>
            </div>
        </>
    )
}


function EnterpriseRow(props) {
    const enterprise = props.enterprise
    const enterpriseLink = `/enterprise/${enterprise.enterpriseId}`
    const { t, i18n } = useTranslation();

        return (
            <tr key={enterprise.enterpriseId.toString()}>
                <th scope="row"><Link to={enterpriseLink}>{enterprise.enterpriseId}</Link></th>
                <td>{enterprise.name}</td>
                <td>{enterprise.rate}</td>
            </tr>
        )
    }

class Statistics extends Component {

    constructor() {
        super();
        this.state = { enterprises: [], username: "" };

    }

    async componentDidMount() {
        var user = await authService.getUser();
        const username = user.preferred_username;
        this.state.username = username;
        document.title = "Statistics";
        if (username == "admin@gmail.com") {
            if (ComboboxSort.current_stat == 0) {
                this.updateEnterprisesAdminHandler();
            }
            else {
                this.updateEnterpriseHandler();
            }
        }
        else {
            this.updateEnterpriseHandler(user.sub);

        }
    }

    // for admin
    updateEnterprisesAdminHandler = () => StatisticsApi.getStatistics(
        enterprises => this.setState({ enterprises: enterprises }));

    //addEnterpriseAdminHandler = (enterprise) => EnterpriseApi.addEnterprise(
    //    enterprise, this.updateEnterprisesAdminHandler);

    //deleteEnterpriseHandler = (enterpriseId) => EnterpriseApi.DeleteEnterpriseById(
    //    enterpriseId, this.updateEnterprisesAdminHandler);

    //for user
    updateEnterpriseHandler = () => StatisticsApi.getStatistics(
        enterprises => this.setState({ enterprises: enterprises }));

    updateReverseEnterpriseHandler = () => StatisticsApi.getReversedStatistics(
        enterprises => this.setState({ enterprises: enterprises }));
    //addEnterpriseHandler = (enterprise) => EnterpriseApi.addEnterprise(
    //    enterprise, this.updateEnterpriseHandler);

    //deleteEnterpriseHandler = (enterpriseId) => EnterpriseApi.DeleteEnterpriseById(
    //    enterpriseId, this.updateEnterpriseHandler);


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
                                    <i className="fa fa-align-justify"></i> {t("Statistics")}
                                </CardHeader>
                                <CardBody>
                                    <Table responsive hover>
                                        <thead>
                                            <tr>
                                                <th scope="col">№</th>
                                                <th scope="col">{t("Enterprise Name")}</th>
                                                <th scope="col">{t("Rate")}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {this.state.enterprises.map((enterprise, index) =>
                                                <EnterpriseRow key={index} enterprise={enterprise} />
                                            )}
                                        </tbody>
                                    </Table>
                                </CardBody>
                            </Card>
                        </Col>
                    </Row>
                    {isAdmin ? (
                        <Button class="btn btn-primary" style={{ marginTop: 20 }}>
                            <Link tag={Link} className="text-dark" to="/enterprise-add/" >{t("Set Statistics")}</Link>
                        </Button>
                    ) : (<></>)}
                    
                </div>
            )
    }
}



export default withTranslation()(Statistics);
