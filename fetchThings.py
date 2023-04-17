import firebase_admin
from firebase_admin import credentials
from firebase_admin import storage
import os

try:
    cred = credentials.Certificate(os.path.join(os.getcwd(), "thingthingthing-script-input-firebase-adminsdk-ws02f-f462ba8163.json"))
    firebase_admin.initialize_app(cred, {
        'storageBucket': 'thingthingthing-script-input.appspot.com'
    })

    bucket = storage.bucket()

    blobs = list(bucket.list_blobs(prefix="scripts/"))

    for blob in blobs:
        filename = blob.name
        filename = filename.replace("scripts/", "")
        if filename.strip() == "":
            continue
        print(filename)
        filepath = os.path.join(os.getcwd(), "Assets/Resources/Things", filename)
        
        try:
            blob.download_to_filename(filepath)
        except Exception as e:
            print(f"Failed to download {filename}: {e}")
except Exception as e:
    print(f"An error occurred: {e}")
