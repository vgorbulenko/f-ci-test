
env.wsPath = "C:\\Jenkins_workspace\\test-pipeline"

node('master') {
	ws( env.wsPath ) {
		stage ('Debud stage') {
			bat """
			echo Current branch name is $BRANCH_NAME
		"""
		}
	}
}

//TODO Versioning
// master release stage
// r- stage


if (BRANCH_NAME == "master" || BRANCH_NAME == "release" || BRANCH_NAME.startsWith('r-')) {

	node('master') {
		ws( env.wsPath ) {
			stage('Checkout on master.') {
				def scmVars = checkout scm
				env.GIT_URL = scmVars.GIT_URL
				bat """
				    @echo off
					echo scmVars
				
				"""
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
			    env.GIT_URL = env.GIT_URL.replace('https://','')
				bat 'echo prepare new release tag'
                    //withCredentials([usernamePassword(credentialsId: 'vgorbulenko_https_github', passwordVariable: 'USERPASS', usernameVariable: 'USERNAME')]) {
					withCredentials([usernamePassword(credentialsId: 'vgorbulenko_token_github', passwordVariable: 'USERPASS', usernameVariable: 'USERNAME')]) {
                        env.VERSION = '0.0.' + BUILD_NUMBER
						bat """
							//@echo off
							git config --global user.email "generate-ci@frustum.io"
							git config --global user.name "Generate CI"
							git tag -a rc-$env.VERSION -m \'autotag\'
							::git push rc-$env.VERSION
							git push https://%USERPASS%@%GIT_URL% rc-$env.VERSION
							::git push +refs/heads/release:refs/remotes/origin/release
						"""
                    }

				
			}		
		}
	}
}


if ( BRANCH_NAME.startsWith('r-') ) {
	node ('master') {
		ws( env.wsPath ) {
			stage ('Publishing installers to PROD bucket') {
				bat """
					echo THIS IS THE TAAAAAAAAAAAAAAAAAAAAAAG!!!!!!!!!! === $BRANCH_NAME ===
				"""
			}
		}
	}
}



//node('slave') {
  //  ws( env.wsPath ) {
	//    bat """ %SCRIPTS-DIR%\\sign.bat generate C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
//		bat """   %SCRIPTS-DIR%\\sign.bat msi C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
	//	bat """   %SCRIPTS-DIR%\\sign.bat package C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""	
//	}
//}