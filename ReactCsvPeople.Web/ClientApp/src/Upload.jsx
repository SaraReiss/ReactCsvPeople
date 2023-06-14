import React, { useRef } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';


const toBase64 = file => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
}

const Upload = () => {
    const navigate = useNavigate();
    const fileRef = useRef(null);

    const onUploadClick = async () => {
        const file = fileRef.current.files[0];
        const base64 = await toBase64(file);
        await axios.post('/api/fileupload/upload', { base64, name: file.name });
        navigate('/');


       
    }

  return (
    <div className="d-flex vh-100" style={{ marginTop: '-70px' }}>
      <div className="d-flex w-100 justify-content-center align-self-center">
        <div className="row">
          <div className="col-md-10">
            <input ref={fileRef} type="file" className="form-control" />
          </div>
          <div className="col-md-2">
            <button className="btn btn-primary" onClick={onUploadClick}>
              Upload
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Upload;
