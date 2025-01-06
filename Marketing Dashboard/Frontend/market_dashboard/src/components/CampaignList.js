import React, {useEffect, useState } from "react";

import '../styles/globals.css'; // Ensure global styles are linked
import CircularProgressWithLabel from '../components/CircularProgressWithLabel'
import LeadsTable from '../components/LeadsTable'
import Box from '@mui/material/Box';
import Collapse from '@mui/material/Collapse';
import IconButton from '@mui/material/IconButton';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import {fetchResponseByCampaignId, fetchAudienceByCampaignId}  from "../lib/api";
import ResponsesTable from '../components/ResponsesTable'


function CampaignRow({ campaign }) {
  const [open, setOpen] = useState(false);
  const [leads, setLeads] = useState([]);
  const [responses, setResponses] = useState([]);


  useEffect(() => {
    if (open) {
      const loadData = async () => {
                  try {
                      const leadsData = await fetchAudienceByCampaignId(campaign.campaignId);
                      setLeads(leadsData);

                      const responsesData = await fetchResponseByCampaignId(campaign.campaignId);
                      setResponses(responsesData);
                  } catch (error) {
                      console.error("Error in loadData:", error.message);
                  }
              };
      
              loadData();
    }
  }, [open, campaign.campaignId]);

  return (
    <>
      <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
        <TableCell>
          <IconButton
            aria-label="expand row"
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell component="th" scope="row">
          {campaign.campaignId}
        </TableCell>
        <TableCell align="right">{campaign.name}</TableCell>
        <TableCell align="right">{campaign.totalLeads}</TableCell>
        <TableCell align="right">{campaign.totalResponses}</TableCell>
        <TableCell align="right">{campaign.fundedLeads}</TableCell>
        <TableCell align="right">
        <CircularProgressWithLabel value=  {(campaign.percentLeadsWithResponse * 100).toFixed(2)} />
        </TableCell>
        <TableCell align="right">
        <CircularProgressWithLabel value= {(campaign.percentLeadsWithFunded * 100).toFixed(2)} />
        </TableCell>
        <TableCell align="right">
        <CircularProgressWithLabel value= {(campaign.percentResponsesWithFunded * 100).toFixed(2)} />
        </TableCell>
        
        
        
        
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <h3>Leads Table</h3>
            <Box sx={{ margin: 1 }}>
              <LeadsTable leads= {leads}/>
            </Box>
            <Box sx={{ margin: 1 }}>
              <h3>Response Table</h3>
              <ResponsesTable responses = {responses}/>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </>
  );
}

export default function CampaignList({ campaigns }) {
  return (
    <section className="campaigns-section">
      <h2>Campaign Statistics</h2>
      <TableContainer component={Paper}>
        <Table aria-label="campaign collapsible table">
          <TableHead>
            <TableRow>
              <TableCell />
              <TableCell>Campaign ID</TableCell>
              <TableCell align="right">Name</TableCell>
              <TableCell align="right">Total Leads</TableCell>
              <TableCell align="right">Total Responses</TableCell>
              <TableCell align="right">Funded Leads</TableCell>

              <TableCell align="right">Percent Leads with Response</TableCell>
              <TableCell align="right">Percent Leads with Funded</TableCell>
              <TableCell align="right">Percent Responses with Funded</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {campaigns.length === 0 ? (
              <TableRow>
                <TableCell colSpan={6}>No campaigns available.</TableCell>
              </TableRow>
            ) : (
              campaigns.map((campaign) => (
                <CampaignRow key={campaign.campaignId} campaign={campaign} />
              ))
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </section>
  );
}
