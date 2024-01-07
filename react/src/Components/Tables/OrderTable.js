import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { styled } from '@mui/material/styles';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Form from '../Forms/CreateProductForm.js';


const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.common.black,
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
  '&:nth-of-type(odd)': {
    backgroundColor: theme.palette.action.hover,
  },
  // hide last border
  '&:last-child td, &:last-child th': {
    border: 0,
  },
}));

function createData(name, description, price, quantity, customerName, productsName) {
  return { name, description, price, quantity , customerName, productsName};
}

export default function OrderTable() {
  const [rows, setRows] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:7126/Order/GetAll')
      .then(response => {
        const newRows = response.data.map(item => createData(item.name,item.description,item.price,item.quantity,item.customerName,item.productsName));
        setRows(newRows);
        console.log(response.data,"order getAll");
      })
      .catch(error => {
        console.log(error);
      });
  }, []);
  
  const handleNewOrder = (newOrder) => {
    setRows(prevRows => [...prevRows, createData(newOrder.name,newOrder.description,newOrder.price,newOrder.quantity,newOrder.customerName,newOrder.productsName)]);
  };

  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 1000 }} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>Name</StyledTableCell>
            <StyledTableCell>Description</StyledTableCell>
            <StyledTableCell>Price(₺)</StyledTableCell>
            <StyledTableCell>Quantity</StyledTableCell>
            <StyledTableCell>Customer Name</StyledTableCell>
            <StyledTableCell>Products Name</StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {rows.map((row) => (
            <StyledTableRow key={row.name}>
              <StyledTableCell component="th" scope="row">
                {row.name}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.description}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.price}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.quantity}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.customerName}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.productsName}
              </StyledTableCell>
            </StyledTableRow>
          ))}
        </TableBody>
        {/* <Form onNewOrder={handleNewOrder}/> */}
      </Table>
    </TableContainer>
  );
}
