import React, { useState, useEffect } from "react";
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup";
import { useDispatch, useSelector } from "react-redux";
import { assetUpdate } from "../slices/assetSlice";

const schema = yup.object().shape({
  name: yup.string().required(),
  email: yup.string().required(),
  phone: yup.string().required(),
  website: yup.string().required(),

}).required();

const MaxWidthDialog = ({ isDialogOpened, handleCloseDialog, items }) => {

  const dispatch = useDispatch();
  const [fullWidth, setFullWidth] = React.useState(true);
  const [maxWidth] = React.useState("sm");
  const [asset, setAssets] = useState([])
  const { register, handleSubmit, formState: { errors, isDirty, isValid } } = useForm({
    mode: 'onChange',
    resolver: yupResolver(schema)
  });

  useEffect(() => {
    setAssets(items)
  })


  const handleClose = () => {
    handleCloseDialog(false);
  };

  const onSubmit = (data) => {
   dispatch(assetUpdate({id: data.id, data: data})).unwrap().then(response => {
    window.location.reload(false);
  })
  .catch(e => {
    console.log(e);
  });
  }

  return (
    <React.Fragment>
      <Dialog
        fullWidth={fullWidth}
        maxWidth={maxWidth}
        open={isDialogOpened}
        onClose={handleClose}
        aria-labelledby="max-width-dialog-title"
      >
        <form onSubmit={handleSubmit(onSubmit)}>
          <DialogTitle>Update Asset</DialogTitle>
          <DialogContent dividers>
            <TextField
              autoFocus
              margin="dense"
              id="id"
              label="ID"
              fullWidth
              variant="outlined"
              inputProps={
                register('id')
              }
              defaultValue={items?.id}
            />
            <TextField
              margin="dense"
              id="name"
              label="Name"
              name="name"
              fullWidth
              variant="outlined"
              inputProps={register('name')}
              defaultValue={items?.name || ''}
            />
            <p style={{ color: 'red' }}>{errors.name?.message}</p>
            <TextField
              margin="dense"
              id="email"
              label="Email"
              fullWidth
              variant="outlined"
              inputProps={register('email')}
              defaultValue={items?.email || ''}
            />
            <p style={{ color: 'red' }}>{errors.brandname?.message}</p>
            <TextField
              margin="dense"
              id="phone"
              label="Phone"
              fullWidth
              variant="outlined"
              inputProps={register('phone')}
              defaultValue={items?.phone || ''}
            />
            <p style={{ color: 'red' }}>{errors.desc?.message}</p>
            <TextField
              margin="dense"
              id="website"
              label="Website"
              fullWidth
              variant="outlined"
              inputProps={register('website')}
              defaultValue={items?.website || ''}
            />
            <p style={{ color: 'red' }}>{errors.price?.message}</p>
          </DialogContent>
          <DialogActions>
            <Button variant="outlined" onClick={handleClose}>Cancel</Button>
            <Button variant="contained" type="submit" disabled={!isValid}>Update</Button>
          </DialogActions>
        </form>
      </Dialog>
    </React.Fragment>
  );
}

export default MaxWidthDialog
