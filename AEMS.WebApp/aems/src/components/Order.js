import * as React from 'react';
import {useState, useEffect} from 'react'
import { DataGrid } from '@mui/x-data-grid';
import {useSelector, useDispatch} from 'react-redux'
import {assetFetch} from '../slices/assetSlice'
import MaxWidthDialog from './editAsset';

const columns = [
  { field: 'name', headerName: 'Name', flex: 1},
  { field: 'businessVersion', headerName: 'Business Version', flex: 1},
  { field: 'code', headerName: 'Code', flex: 1},
  { field: 'imei', headerName: 'Imei', flex: 1},
  { field: 'managementVersion', headerName: 'Management Version', flex: 1,},
];

const DataTable = ()  => {
  const dispatch = useDispatch();
  const assetItems = useSelector((state) => ({...state.asset}));
  const [isOpen, setIsOpen] = useState(false);
  const [selectedRows, setSelectedRows] = React.useState();

  useEffect(() => {
     dispatch(assetFetch)
     console.log('asset fetch', assetItems)
  })

  const selectedModelChange = ids => {
    setSelectedRows(ids)
    setIsOpen(!isOpen)
  }
  return (
    <div style={{ height: 400, width: '100%' }}>
      <DataGrid
        rows={assetItems?.items?.resData}
        columns={columns}
        pageSize={5}
        rowsPerPageOptions={[5]}
        onSelectionModelChange={(ids) => {selectedModelChange(ids)}}
      />
      <MaxWidthDialog isDialogOpened={isOpen} id={selectedRows} handleCloseDialog={() => setIsOpen(false)} />
    </div>
  );
}

export default DataTable;
