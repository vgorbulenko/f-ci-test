
env.wsPath = "C:\\Jenkins_workspace\\test-pipeline"

//def receive() {
//  env.event_type = request.headers["X-GitHub-Event"]
//  env.payload    = request.body
//}
//env.eventType = event_type

if (BRANCH_NAME == "master" || BRANCH_NAME == "release") {

	node('master') {
		ws( env.wsPath ) {
			stage('Checkout on master') {
				checkout scm
			}

			stage('Building stage') {
               bat 'echo Build there something============'
			}
		}
	}
}	


if (BRANCH_NAME == "release") {
	node ('master') {
		ws (env.wsPath) {
			stage ('Push new tag to GitHub') {
			    bat 'echo prepare new release tag'
			
			}		
		}
	}
}


if ( BRANCH_NAME.startsWith('r-') ) {
    bat 'echo THIS IS THE TAAAAAAAAAAAAAAAAAAAAAAG!!!!!!!!!! === $BRANCH_NAME ==='
}



//node('slave') {
  //  ws( env.wsPath ) {
	//    bat """ %SCRIPTS-DIR%\\sign.bat generate C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
//		bat """   %SCRIPTS-DIR%\\sign.bat msi C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
	//	bat """   %SCRIPTS-DIR%\\sign.bat package C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""	
//	}
//}