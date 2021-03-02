import firebase_admin
from firebase_admin import credentials
from firebase_admin import storage
import os

cred = credentials.Certificate(os.path.join(os.getcwd(),"thingthingthing-script-input-firebase-adminsdk-ws02f-f77579a5d6.json"))
firebase_admin.initialize_app(cred, {
    'storageBucket': 'thingthingthing-script-input.appspot.com'
})

bucket = storage.bucket()

blobs = list(bucket.list_blobs(prefix="scripts/"))



for blob in blobs:
    filename = blob.name
    # print(filename)
    filename = filename.replace("scripts/","")
    if filename.strip()=="":
        continue
    print(filename)
    filepath = os.path.join(os.getcwd(),"Assets/Scripts/Things",filename)
    blob.download_to_filename(filepath)

    # https://googleapis.dev/python/storage/latest/blobs.html