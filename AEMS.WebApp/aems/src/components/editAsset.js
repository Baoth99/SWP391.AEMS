import React, { useState, useEffect } from "react";
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';

export default function MaxWidthDialog({ isDialogOpened, handleCloseDialog, items }) {
  useEffect(() => {
    handleClickOpen();
  }, []);

  //const classes = useStyles();
  //const [open, setOpen] = React.useState(false);
  const [fullWidth, setFullWidth] = React.useState(true);
  const [maxWidth] = React.useState("sm");

  const handleClickOpen = () => {
    //setOpen(true);
    //setTimeout(() => setOpen(false), 16000);
  };

  const handleClose = () => {
    handleCloseDialog(false);
  };

  /* const handleMaxWidthChange = event => {
    setMaxWidth(event.target.value);
  }; */

  const handleFullWidthChange = (event) => {
    setFullWidth(event.target.checked);
  };

  return (
    <React.Fragment>
      <Dialog
        fullWidth={fullWidth}
        maxWidth={maxWidth}
        open={isDialogOpened}
        onClose={handleClose}
        aria-labelledby="max-width-dialog-title"
      >
       <DialogTitle>Update Asset</DialogTitle>
        <DialogContent dividers>
          <TextField
            autoFocus
            margin="dense"
            id="id"
            label="ID"
            fullWidth
            variant="outlined"
            disabled
            defaultValue={items[0]?.id}
          />
           <TextField
            margin="dense"
            id="name"
            label="Name"
            fullWidth
            variant="outlined"
            defaultValue={items[0]?.name}
          />
           <TextField
            margin="dense"
            id="brandname"
            label="Brand Name"
            fullWidth
            variant="outlined"
            defaultValue={items[0]?.brand}
          />
            <TextField
            margin="dense"
            id="desc"
            label="Desc"
            fullWidth
            variant="outlined"
            defaultValue={items[0]?.desc}
          />
            <TextField
            margin="dense"
            id="price"
            label="Price"
            fullWidth
            variant="outlined"
            defaultValue={items[0]?.price}
          />
        </DialogContent>
        <DialogActions>
          <Button variant="outlined" onClick={handleClose}>Cancel</Button>
          <Button variant="contained">Update</Button>
        </DialogActions>

      </Dialog>
    </React.Fragment>
  );
}
