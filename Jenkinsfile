
env.wsPath = "C:\\Jenkins_workspace\\test-pipeline"

if (BRANCH_NAME == "master" || BRANCH_NAME == "release") {

	node('master') {
		ws( env.wsPath ) {
			stage('Checkout on master. Branch '+BRANCH_NAME) {
				def scmVars = checkout scm
				env.GIT_URL = scmVars.GIT_URL
			}

			stage('Building stage') {
               bat 'echo Build there something============'
			}
			
			stage ('Parsing URL') {
			    bat """
				   @echo off
				   echo env.GIT_URL
				   env.GIT_URL = env.GIT_URL.replace('https://','')
				   echo env.GIT_URL
				
				"""
			
			}
		}
	}
}	


if (BRANCH_NAME == "release") {
	node ('master') {
		ws (env.wsPath) {
			stage ('Push new tag to GitHub') {
			    bat 'echo prepare new release tag'

                        withCredentials([usernamePassword(credentialsId: 'vgorbulenko_https_github', passwordVariable: 'USERPASS', usernameVariable: 'USERNAME')]) {
                            bat """@echo off
                            set /p version=0.0.$BUILD_NUMBER
                        //    C:\\Progra~1\\Git\\cmd\\git.exe tag rel-v%version% -m \'autotag\'
                        //    C:\\Progra~1\\Git\\cmd\\git.exe push https://%USERNAME%:%USERPASS%@gitlab.amcbridge.com/spronyuk/pipeline-dev-repo.git rel-v%version%"""
                        }

				
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