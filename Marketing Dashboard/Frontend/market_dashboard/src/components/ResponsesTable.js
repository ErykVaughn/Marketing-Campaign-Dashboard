import React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

export default function ResponsesTable({ responses }) {
  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="responses table">
        <TableHead>
          <TableRow>
            <TableCell>Campaign ID</TableCell>
            <TableCell>Record ID</TableCell>
            <TableCell>Lead Flag</TableCell>
            <TableCell>Lead Timestamp</TableCell>
            <TableCell>Funded Flag</TableCell>
            <TableCell>Funded Timestamp</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {responses.map((response, index) => (
            <TableRow
              key={index}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell>{response.campaign_ID}</TableCell>
              <TableCell>{response.record_ID}</TableCell>
              <TableCell>{response.lead_Flag ? 'Yes' : 'No'}</TableCell>
              <TableCell>
                {response.lead_Timestamp
                  ? new Date(response.lead_Timestamp).toLocaleString()
                  : 'N/A'}
              </TableCell>
              <TableCell>{response.funded_Flag ? 'Yes' : 'No'}</TableCell>
              <TableCell>
                {response.funded_Timestamp
                  ? new Date(response.funded_Timestamp).toLocaleString()
                  : 'N/A'}
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
