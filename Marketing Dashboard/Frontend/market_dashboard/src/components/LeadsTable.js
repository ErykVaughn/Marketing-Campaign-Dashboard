import React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

export default function LeadsTable({ leads }) {
  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="leads table">
        <TableHead>
          <TableRow>
            <TableCell>Campaign ID</TableCell>
            <TableCell>Record ID</TableCell>
            <TableCell>Has Phone</TableCell>
            <TableCell>Has Email</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {leads.map((lead, index) => (
            <TableRow
              key={index}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell>{lead.campaign_ID}</TableCell>
              <TableCell>{lead.record_ID}</TableCell>
              <TableCell>{lead.has_Phone ? 'Yes' : 'No'}</TableCell>
              <TableCell>{lead.has_Email ? 'Yes' : 'No'}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
